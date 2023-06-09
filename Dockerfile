# 使用基于Ubuntu的.NET镜像
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal

# 设置工作目录
WORKDIR /app

# 安装MySQL
RUN apt-get update && apt-get install -y mysql-server

# 启动MySQL，修改root用户密码，创建数据库和用户
RUN service mysql start && \
    mysql -u root -e "ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'iiLvWUGkiTgq8zL';FLUSH PRIVILEGES;" && \
    mysql -u root -p'iiLvWUGkiTgq8zL' -e "CREATE DATABASE ShortLinkDB;CREATE USER 'shortlink'@'%' IDENTIFIED BY 'shortlink';GRANT ALL PRIVILEGES ON ShortLinkDB.* TO 'shortlink'@'%';FLUSH PRIVILEGES;"

# 构建.NET项目
COPY ShortLinkGeneration/*.csproj ./
RUN dotnet restore

COPY ShortLinkGeneration/ ./
RUN dotnet publish -c Release -o out

# 安装Node.js，构建Vue项目
RUN apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y nodejs

RUN node --version
RUN npm --version

WORKDIR /app/vue

COPY short-link-generation-vue/package*.json ./
RUN npm install

COPY short-link-generation-vue/ ./
RUN npm run build

# 复制Vue的构建结果到.NET项目的wwwroot目录下
WORKDIR /app
RUN mkdir -p /var/www/html
RUN cp -r vue/dist/* /var/www/html

# 安装Nginx
RUN apt-get install -y nginx

# 添加Nginx配置文件
COPY nginx.conf /etc/nginx/sites-available/default

# 配置启动脚本
COPY run-docker.sh .
RUN chmod +x run-docker.sh

# 开始服务
CMD ["./run-docker.sh"]
