# 使用基于Ubuntu的.NET运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# 设置工作目录
WORKDIR /app

# 安装MariaDB
RUN apt-get update && apt-get install -y mariadb-server

# 复制.net项目
RUN mkdir -p ./dotnet-build
COPY dotnet-build/ ./dotnet-build

# 安装Nginx
RUN apt-get install -y nginx

# 添加Nginx配置文件
COPY nginx.conf /etc/nginx/sites-available/default

# 复制Vue
RUN mkdir -p /var/www/html
COPY vue-build/ /var/www/html

# 配置启动脚本
COPY run-docker.sh .
RUN chmod +x run-docker.sh

# 开始服务
CMD ["./run-docker.sh"]
