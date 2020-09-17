# console 小游戏

## 目标

玩家进入开始场景 ，开始场景 输入姓名后 点击创建房间 ，或者加入房间   等待其它玩家加入 ，房主开始游戏 加入游戏场景 ，游戏场景玩家可通过 上下左右 移动 按 空格 释放攻击
 敌人扣除血量

## 问题拆分
####场景 scence
###### 登录场景
username 新增 修改 
跳转 房间场景
###### 房间场景
房间列表 输入房间id加入房间 返回登陆场景

玩家列表 主机 开始游戏，退出房间  剔除人员 ，升级房主
###### 游戏场景
x*y 二位数组
fps 每秒刷新次数
sprite list add remove

####房间
id sockets
###### 房间列表
###### 房间用户列表
####用户
hp
position
attatch
direction
####技能
damage
position
direction
speed
####网络
ping pong 
move 
attach
room add remove update list 
start

