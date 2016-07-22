#!/bin/bash
LOGTIME=$(date +%Y%m%d_%H%M%S)
erl -pa ebin/ -boot start_sasl -sasl sasl_error_logger "{file, \"logs/server_""$LOGTIME"".log\"}" -sname uzone_server -setcookie uzone_cookie -eval "application:start(uz_server)" -K true
