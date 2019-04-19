#!/bin/bash

isPostgresReady() {
  ./dc.sh exec -T postgres pg_isready
}

runSqlScriptAsPostgres() {
  echo "Applying SQL file: $1" 
  cat $1 | ./dc.sh exec -T postgres psql -U postgres
}

runSqlScript() {
  echo "Applying SQL file: $1" 
  cat $1 | ./dc.sh exec -T postgres psql -U todo_admin todo
  cat $1 | ./dc.sh exec -T postgres psql -U todo_admin todo_integration
}

echo "Waiting for postgres to be ready"
isPostgresReady
while [[ "$?" != "0" ]]
do
    sleep 3
    isPostgresReady
done

runSqlScriptAsPostgres ./postgres/create_users.sql
runSqlScriptAsPostgres ./postgres/create_databases.sql
runSqlScript ./postgres/grant_app_permissions.sql
runSqlScript ./postgres/create_tables.sql
runSqlScript ./postgres/seed_tables.sql
