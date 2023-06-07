# 短连接生成系统

## 项目介绍

本项目为前后端分离的短连接生成系统

前端采用`vite + vue3`技术栈

后端采用`.NET Core 6`技术栈

## 项目部署

// TODO

## 项目开发

<details><summary>点击展开</summary></summary>
<p>

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

## 前端

// TODO

## 后端

### 接口设计

<details><summary>点击展开</summary></summary>
<p>

#### [接口文档[TODO]](https://console-docs.apipost.cn/preview/8d1c74a51bbe0958/b40c0ea6a6e69f78)

#### 初始化

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

#### 链接重定向

- ✅ POST `api/to/redirect`：重定向到指定的短链接的原始链接。
- ✅ GET `/{shortLink}`：重定向到指定的短链接的原始链接。

#### 管理员用户管理

- POST `api/admin/users`：获取所有用户的信息
- POST `api/admin/users/create`：创建一个新的用户，请求体包含新用户的信息
- POST `api/admin/users/get`：获取指定用户的信息，请求体包含用户ID
- POST `api/admin/users/update`：更新指定用户的信息，请求体包含用户ID和要更新的信息
- POST `api/admin/users/delete`：删除指定用户，请求体包含用户ID

#### 管理员链接管理

- POST `api/admin/links`：获取所有链接的信息
- POST `api/admin/links/get`：获取指定链接的信息，请求体包含链接ID
- POST `api/admin/links/update`：更新指定链接的信息，请求体包含链接ID和要更新的信息
- POST `api/admin/links/delete`：删除指定链接，请求体包含链接ID
- POST `api/admin/links/disabled`：禁用链接

</p>
</details>

### 数据库设计

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