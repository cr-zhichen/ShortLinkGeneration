#!/bin/bash

# 设置固定的镜像名和容器名
IMAGE_NAME="short-link-generation-dev"
CONTAINER_NAME="short-link-generation-dev"

# 提示用户输入信息，并检查输入是否为空
read -p "请输入鉴权Key：" SECRET_KEY
if [[ -z "$SECRET_KEY" ]]; then
  echo "鉴权Key不能为空"
  exit 1
fi

read -p "请输入发行者：" ISSUER
if [[ -z "$ISSUER" ]]; then
  echo "发行者不能为空"
  exit 1
fi

read -p "请输入Audience：" AUDIENCE
if [[ -z "$AUDIENCE" ]]; then
  echo "Audience不能为空"
  exit 1
fi

read -p "请输入有效时间（分钟）：" EXPIRE_MINUTES
if [[ -z "$EXPIRE_MINUTES" ]]; then
  echo "有效时间不能为空"
  exit 1
fi

read -p "请输入发件服务器地址：" SMTP_SERVICE
if [[ -z "$SMTP_SERVICE" ]]; then
  echo "发件服务器地址不能为空"
  exit 1
fi

read -p "请输入SMTP端口：" SMTP_PORT
if [[ -z "$SMTP_PORT" ]]; then
  echo "SMTP端口不能为空"
  exit 1
fi

read -p "请输入发件人地址：" SEND_EMAIL
if [[ -z "$SEND_EMAIL" ]]; then
  echo "发件人地址不能为空"
  exit 1
fi

read -p "请输入发件密码：" SEND_PWD
if [[ -z "$SEND_PWD" ]]; then
  echo "发件密码不能为空"
  exit 1
fi

# 构建Docker镜像
docker build -t $IMAGE_NAME .

# 运行Docker容器，同时设置环境变量
docker run -d \
--name $CONTAINER_NAME \
-p 9000:80 \
-e ConnectionStrings__ShortLinkContext="Server=127.0.0.1;Port=3306;Database=ShortLinkDB;User=shortlink;Password=shortlink;" \
-e TokenOptions__SecretKey="$SECRET_KEY" \
-e TokenOptions__Issuer="$ISSUER" \
-e TokenOptions__Audience="$AUDIENCE" \
-e TokenOptions__ExpireMinutes=$EXPIRE_MINUTES \
-e SMTP__SmtpService="$SMTP_SERVICE" \
-e SMTP__Port=$SMTP_PORT \
-e SMTP__SendEmail="$SEND_EMAIL" \
-e SMTP__SendPwd="$SEND_PWD" \
$IMAGE_NAME
