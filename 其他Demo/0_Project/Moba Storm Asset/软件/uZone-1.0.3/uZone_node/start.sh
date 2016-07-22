#!/bin/bash
LOGTIME=$(date +%Y%m%d_%H%M%S)
erl -pa ebin/ -boot start_sasl -sasl sasl_error_logger "{file, \"logs/node_""$LOGTIME"".log\"}" -sname uzone_node -setcookie uzone_cookie -eval 'application:start(uz_node)' -K true
