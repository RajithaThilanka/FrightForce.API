INSERT INTO ff_document_types
( name, code, created_at, created_by, updated_at, updated_by, is_deleted, deleted_at)
VALUES
    ('Bill of Lading', 'BL', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ( 'Packing List', 'PL', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ( 'Import License', 'IL', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ( 'Export License', 'EL', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Customs Declaration', 'CD', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Cargo Manifest', 'CM', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Certificate of Origin', 'CO', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ( 'Inspection Certificate', 'IC', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Hazardous Materials Doc', 'HMD', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Insurance Document', 'ID', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ( 'Freight Invoice', 'FI', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Container Load Plan', 'CLP', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Seal Number Record', 'SNR', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Temperature Log', 'TL', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL),
    ('Delivery Order', 'DO', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', false, NULL);


ALTER SEQUENCE public.ff_document_types_id_seq RESTART WITH 100;