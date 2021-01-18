# PMChat

> * 基于**DotNet5+SuperSocket**的数据传输Demo
> * Client端采用WPF, Server端采用控制台
> * 在原有的基础上集成了Udp便捷开发工具
>> * 对数据进行切块->切片->封装等操作
>> * 数据切片校验
>> * 数据块校验
>> * 丢包重传 (进度60%)
>> * 流量控制 (进度70%)
>> * 校验思路: 数据传输交给UDP, 校验交给TCP
> * 在局域网环境下表现良好

### PMChat.Client
> 实现功能:
> * 多对多聊天
> * 一对一聊天
> * 一对一发图片
> * 一对一文件传输

> 待实现功能:
> * 一对多广播多媒体文件

### PMChat.Model
> 交互数据包设计, 中间层实现等

### PMChat.Server
> 实现功能:
> * 基本信息记录
> * 消息分发
> * 客户端分配
> * 多媒体流终端配置中转
> * 等等

> 待实现功能:
> * 多播信息分发优化
> * 数据库交互等