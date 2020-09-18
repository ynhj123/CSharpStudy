# console 小游戏
ECS，即 Entity-Component-System（实体-组件-系统） 的缩写，其模式遵循组合优于继承原则，
游戏内的每一个基本单元都是一个实体，每个实体又由一个或多个组件构成，
每个组件仅仅包含代表其特性的数据（即在组件中没有任何方法），
例如：移动相关的组件MoveComponent包含速度、位置、朝向等属性，
一旦一个实体拥有了MoveComponent组件便可以认为它拥有了移动的能力，
系统便是来处理拥有一个或多个相同组件的实体集合的工具，
其只拥有行为（即在系统中没有任何数据），在这个例子中，处理移动的系统仅仅关心拥有移动能力的实体，
它会遍历所有拥有MoveComponent组件的实体，并根据相关的数据（速度、位置、朝向等），更新实体的位置。
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
speed
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

