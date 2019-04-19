CREATE TABLE todo_items (
    item_id text NOT NULL PRIMARY KEY,
    item_text text NOT NULL,
    created_at timestamp with time zone NOT NULL,
    completed_at timestamp with time zone
);
