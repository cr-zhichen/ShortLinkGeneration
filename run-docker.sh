#!/bin/bash

# 启动MySQL
service mysql start

# 启动Nginx
service nginx start

# 启动.NET应用程序
dotnet /app/out/ShortLinkGeneration.dll

