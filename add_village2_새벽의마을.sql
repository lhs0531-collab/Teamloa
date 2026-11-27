-- 새벽의마을(마을 번호 2) DB 데이터 추가
--
-- 2번 마을 데이터가 없는 DB(개발용 DB, lastking.template.sql로 만든 DB)에만 필요하다.
-- 이미 3마을로 운영 중인 DB는 데이터가 있으므로 각 구문이 아무것도 넣지 않는다(다시 실행해도 중복되지 않는다).
--
-- 영혼의마을(0번)의 마을 전용 데이터를 복사해 이름만 '새벽의마을'로 바꿔 넣는다.
-- 대상: vilagemaster, mapmaster(마을 맵 12장), gotocmd(@이동 명령), portalmaster(마을 포탈)
-- 몬스터 스폰은 서버 코드(SpawnerMonster)가 마을 수만큼 자동으로 만든다.
--
-- 순서: 1) DB 백업  2) 이 스크립트 실행  3) 서버 시작
-- 2번 마을 맵이 없는 채로 서버를 띄우면 콘솔에 "[오류] 2번 마을의 맵 ... 없습니다"가 찍히고 기동 중 멈춘다.

SET NAMES utf8;
START TRANSACTION;

-- 1. 마을 등록
INSERT INTO `vilagemaster` (`IDX`, `NAME`, `isOpen`)
SELECT 2, '새벽의마을', 'FALSE' FROM DUAL
WHERE NOT EXISTS (SELECT 1 FROM (SELECT `IDX` FROM `vilagemaster`) v WHERE v.`IDX` = 2);

-- 2. 마을 맵 (IDX는 자동 증가가 아니라서 현재 최댓값 뒤로 이어 붙인다)
SET @idx := (SELECT MAX(`IDX`) FROM `mapmaster`);
INSERT INTO `mapmaster` (`IDX`, `VilgNum`, `MAPNAME`, `FILENAME`, `Fog`, `Sound`, `LimitLevel`, `isPK`, `isOpen`, `WorldTime`, `isLegend`, `DC_YN`)
SELECT (@idx := @idx + 1), 2, REPLACE(src.`MAPNAME`, '영혼의마을', '새벽의마을'),
       src.`FILENAME`, src.`Fog`, src.`Sound`, src.`LimitLevel`, src.`isPK`, src.`isOpen`, src.`WorldTime`, src.`isLegend`, src.`DC_YN`
FROM (SELECT * FROM `mapmaster` WHERE `VilgNum` = 0 ORDER BY `IDX`) src
WHERE NOT EXISTS (SELECT 1 FROM (SELECT `VilgNum` FROM `mapmaster`) m WHERE m.`VilgNum` = 2);

-- 3. @이동 명령
INSERT INTO `gotocmd` (`VilgNum`, `GoName`, `Map`, `LocX`, `LocY`)
SELECT 2, src.`GoName`, REPLACE(src.`Map`, '영혼의마을', '새벽의마을'), src.`LocX`, src.`LocY`
FROM (SELECT * FROM `gotocmd` WHERE `VilgNum` = 0 ORDER BY `IDX`) src
WHERE NOT EXISTS (SELECT 1 FROM (SELECT `VilgNum` FROM `gotocmd`) g WHERE g.`VilgNum` = 2);

-- 4. 마을 포탈
INSERT INTO `portalmaster` (`VilgNum`, `From_MAP`, `From_LocX`, `From_LocY`, `To_MAP`, `To_LocX`, `To_LocY`, `DC_YN`)
SELECT 2, REPLACE(src.`From_MAP`, '영혼의마을', '새벽의마을'), src.`From_LocX`, src.`From_LocY`,
       REPLACE(src.`To_MAP`, '영혼의마을', '새벽의마을'), src.`To_LocX`, src.`To_LocY`, src.`DC_YN`
FROM (SELECT * FROM `portalmaster` WHERE `VilgNum` = 0 ORDER BY `IDX`) src
WHERE NOT EXISTS (SELECT 1 FROM (SELECT `VilgNum` FROM `portalmaster`) p WHERE p.`VilgNum` = 2);

-- 4-1. 2번 마을 포탈이 이미 있는데 전부 꺼져(DC_YN='TRUE') 있으면 켠다. (s3 덤프와 template이 이 상태다)
--      서버는 DC_YN='FALSE'인 행만 읽는다. 하나라도 켜져 있으면 손대지 않는다.
UPDATE `portalmaster` SET `DC_YN` = 'FALSE'
WHERE `VilgNum` = 2
  AND NOT EXISTS (SELECT 1 FROM (SELECT `VilgNum`, `DC_YN` FROM `portalmaster`) p WHERE p.`VilgNum` = 2 AND p.`DC_YN` = 'FALSE');

COMMIT;

-- 확인: 세 마을의 행 수가 같아야 한다.
SELECT 'mapmaster' AS tbl, `VilgNum`, COUNT(*) AS cnt FROM `mapmaster` WHERE `VilgNum` IN (0, 1, 2) GROUP BY `VilgNum`
UNION ALL
SELECT 'gotocmd', `VilgNum`, COUNT(*) FROM `gotocmd` WHERE `VilgNum` IN (0, 1, 2) GROUP BY `VilgNum`
UNION ALL
SELECT 'portalmaster', `VilgNum`, COUNT(*) FROM `portalmaster` WHERE `VilgNum` IN (0, 1, 2) AND `DC_YN` = 'FALSE' GROUP BY `VilgNum`;
