#!/bin/bash

./dc.sh down \
  && ./dc.sh up --remove-orphans -d \
  && ./initialize-postgres.sh
