# MyDemoWebApi
## 20251107
### IDE
1. Visual Studio 2022
2. .NET
3. 确保安装了ASP.NET和Web开发, 并勾选了"".Net Framework 项目和项模板"
4. 安装完成后, 重启Visual Studio

### 创建项目
1. 打开Visual Studio 2022, 点击"创建新项目"
2. 选择"ASP.NET Web应用程序(.NET Framework)", 点击"下一步"
3. 填写项目名称"MyDemoWebApi", 选择保存位置, 点击"创建"
4. 在"新建ASP.NET Web应用程序"窗口中, 选择"Web API"模板, 点击"创建"
5. 等待项目创建完成

### 项目 [ref](https://blog.csdn.net/zgscwxd/article/details/133823295)
1. app_start文件夹: 包含应用程序启动配置文件, 如WebApiConfig.cs是配置Web API路由的文件;
2. 在WebApiConfig.cs里的routeTemplate增加"{action}"来实现通过url请求函数名, 避免路由冲突, 而"{id}"就是接口函数的参数;
3. 在Models下创建一个电影模型 Movie.cs;
4. Controller是处理HTTP请求的对象, 在Controllers下创建一个"Web API 2控制器" - 用户控制器MovieController.cs, 实现基本的CRUD操作;
5. 在MovieController.cs里定义Movie数据, 并实现GET, POST, PUT, DELETE方法;
6. 运行项目, 访问API端点, 测试功能;
7. 在url中输入: https://localhost:xxxx/api/{Controller prefix}/{func} (xxxx是你的端口号), 即可访问API;
8. 对与需要参数的函数, 直接在url后面添加参数即可: https://localhost:xxxx/api/{Controller prefix}/{func}?param1=value1&param2=value2;

### 优化
1. 移除XML格式支持, 只保留JSON格式支持;
2. 接口返回格式 - todo;