# NuGet包使用说明

## 错误信息

如果您看到以下错误信息：

```
严重性	代码	说明	项目	文件	行	禁止显示状态	详细信息
错误	This project references NuGet package(s) that are missing on this computer. Use NuGet Package Restore to download them.  For more information, see http://go.microsoft.com/fwlink/?LinkID=322105. The missing file is ..\packages\System.Data.SQLite.Core.1.0.118.0\build\net46\System.Data.SQLite.Core.targets.
```

这表示项目引用了System.Data.SQLite.Core NuGet包，但是这个包在您的电脑上不存在。

## 解决方法

### 自动化解决（推荐）

1. 双击项目根目录下的`restore_packages.bat`文件
2. 等待批处理文件执行完成
3. 重新打开解决方案并构建项目

### 使用Visual Studio手动解决

1. 在Visual Studio中打开解决方案
2. 右键单击解决方案资源管理器中的解决方案
3. 选择"还原NuGet包"
4. 等待还原完成后重新构建项目

### 使用NuGet命令行解决

1. 打开命令提示符或PowerShell
2. 导航到项目目录
3. 执行以下命令：
   ```
   nuget restore myEyeGuard.sln
   ```
4. 重新打开解决方案并构建项目

## 所需的NuGet包

本项目使用以下NuGet包：

| 包名 | 版本 | 用途 |
|------|------|------|
| System.Data.SQLite.Core | 1.0.118.0 | SQLite数据库访问 |
| Microsoft.Extensions.DependencyInjection | 7.0.0 | 依赖注入容器 |
| Microsoft.Extensions.DependencyInjection.Abstractions | 7.0.0 | 依赖注入抽象 |
| System.Numerics.Vectors | 4.4.0 | 数值计算支持 |

## 注意事项

- 如果您在还原包后仍然遇到问题，请尝试清理解决方案（"生成"→"清理解决方案"）然后重新构建
- 确保您的计算机已连接到互联网，以便下载NuGet包
- 如果您在公司网络环境中，可能需要配置NuGet代理设置
- 如果您使用的是自定义NuGet源，请确保已正确配置NuGet.config文件

## 手动下载包

如果自动方法失败，您可以从以下链接手动下载NuGet包：

1. System.Data.SQLite.Core: https://www.nuget.org/packages/System.Data.SQLite.Core/1.0.118.0
2. Microsoft.Extensions.DependencyInjection: https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/7.0.0

下载后，将.nupkg文件解压到项目的packages目录中对应的文件夹。 