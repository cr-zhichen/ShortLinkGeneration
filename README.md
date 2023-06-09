# 短连接生成系统

## 项目介绍

本项目为前后端分离的短连接生成系统

前端采用`vite + vue3`技术栈

后端采用`.NET Core 6`技术栈

## 项目部署

### Docker部署

使用Docker部署前，请确保正确安装并启动Docker

#### Docker构建(最快)

直接执行：

``` shall
curl -LJO https://github.com/cr-zhichen/ShortLinkGeneration/raw/main/Docker.tar.gz && tar -xzf Docker.tar.gz && cd Docker && chmod +x start.sh && ./start.sh
```

#### Docker源码构建(兼容性强)

克隆存储库，运行`start.sh`脚本，根据脚本提示输入配置，即可完成构建。

## 项目开发

<details><summary>后端</summary></summary>
<p>

克隆存储库，并在根目录下运行

### 安装SDK

https://learn.microsoft.com/zh-cn/dotnet/core/install/

### 运行项目

#### 构建项目

```shell
dotnet build ShortLinkGeneration/ShortLinkGeneration.csproj /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary
```

#### 发布项目

```shell
dotnet publish ShortLinkGeneration/ShortLinkGeneration.csproj /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary
```

#### 运行项目

```shell
dotnet watch run --project ShortLinkGeneration/ShortLinkGeneration.csproj
```

或者

``` shell
dotnet run --project ShortLinkGeneration/ShortLinkGeneration.csproj --framework net6.0
```

</p>
</details>


<details><summary>前端</summary></summary>
<p>

// TODO

</p>
</details>

## 接口设计

<details><summary>点击展开</summary></summary>
<p>

#### [接口文档[TODO]](https://console-docs.apipost.cn/preview/8d1c74a51bbe0958/b40c0ea6a6e69f78)

#### 初始化

- ✅ POST `/init/is-init`: 判断是否已经初始化
- ✅ POST `/init/db`: 初始化数据库
- ✅ POST `/init/admin`: 初始化管理员用户

#### 用户管理

- ✅ POST `api/users/register`：用户注册
- ✅ POST `api/users/login`：用户登录
- ✅ POST `api/users/info`：返回当前登录的用户信息
- ✅ POST `api/users/update-password`：更新密码
- ✅ POST `api/users/reset-password`：重置密码
- ✅ POST `api/users/send-email`: 发送验证码

#### 链接管理

- ✅ POST `api/links/create`：生成新的短链接
- ✅ POST `api/links/detection`：检测短连接是否可用
- ✅ POST `api/links/get-all`：获取当前用户创建的所有短链接
- ✅ POST `api/links/get`：获取指定的短链接的信息
- ✅ POST `api/links/update`：更新指定的短链接
- ✅ POST `api/links/delete`：删除指定的短链接
- ✅ POST `api/links/search`：模糊搜索短连接

#### 链接重定向

- ✅ POST `api/to/redirect`：重定向到指定的短链接的原始链接。
- ✅ GET `/{shortLink}`：重定向到指定的短链接的原始链接。

#### 管理员用户管理

- ✅ POST `api/admin/users/get-all`：获取所有用户的信息
- ✅ POST `api/admin/users/create`：创建一个新的用户
- ✅ POST `api/admin/users/get`：获取指定用户的信息
- ✅ POST `api/admin/users/delete`：删除指定用户
- ✅ POST `api/admin/users/reset-password`：重置指定用户的密码
- ✅ POST `api/admin/users/search`：模糊搜索用户

#### 管理员链接管理

- ✅ POST `api/admin/links/get-all`：获取所有链接的信息
- ✅ POST `api/admin/links/get`：获取指定链接的信息
- ✅ POST `api/admin/links/update`：更新指定链接的信息
- ✅ POST `api/admin/links/delete`：删除指定链接
- ✅ POST `api/admin/links/disabled`：禁用链接
- ✅ POST `api/admin/links/search`：模糊搜索链接
- ✅ POST `api/admin/links/get-by-user`：获取指定用户的所有链接
- ✅ POST `api/admin/links/get-clicks`：获取指定链接的点击记录

</p>
</details>

## 数据库设计

<details><summary>点击展开</summary></summary>
<p>

#### Users 表

|  Field Name  |      Field Type       | Description |
|:------------:|:---------------------:|:-----------:|
|    UserID    |          INT          |  主键，唯一标识用户  |
|   Username   |     VARCHAR(255)      | 用户名（邮箱），唯一  |
| PasswordHash |     VARCHAR(255)      |   密码的哈希值    |
|     Role     | ENUM('admin', 'user') |    用户的角色    |
| CreationTime |       DATETIME        |   用户创建时间    |

#### Links 表

|  Field Name  |  Field Type   |     Description      |
|:------------:|:-------------:|:--------------------:|
|    LinkID    |      INT      |      主键，唯一标识链接       |
|  ShortLink   | VARCHAR(255)  |        短链接，唯一        |
| OriginalLink | VARCHAR(2048) |         原始链接         |
|    UserID    |      INT      | 外键，标识这个链接是哪个用户创建的，可空 |
| CreationDate |   DATETIME    |      链接创建的日期和时间      |
|  ClickCount  |      INT      |       链接被点击的次数       |
|  ExpiryDate  |   DATETIME    |       链接的过期时间        |
|  MaxClicks   |      INT      |       链接最大点击次数       |
|  IsDisabled  |     BOOL      |        是否被禁用         |

#### Clicks 表

| Field Name | Field Type  |   Description    |
|:----------:|:-----------:|:----------------:|
|  ClickID   |     INT     |   主键，唯一标识点击事件    |
|   LinkID   |     INT     | 外键，标识这个点击事件关联的链接 |
| ClickTime  |  DATETIME   |     点击的日期和时间     |
|  SourceIP  | VARCHAR(45) |      来源IP地址      |

</p>
</details>