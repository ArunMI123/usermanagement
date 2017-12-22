#!/bin/bash
nohup /usr/bin/filebeat &>/tmp/filebeat.log &
dotnet ARBUserManagement.dll