# SREDemo
__学习项目,用C#模拟软件渲染流程__

# 一些思考

__介绍:__  
原本的渲染流程应该是由GPU来完成的,我们通过编写代码让CPU直接处理了这个流程,以此来熟悉渲染流程.

__一些重要的事:__
C# 的实现基于Graphics类,所有的操作最终都会对Bitmap造成影响，从而改变在屏幕的的表现;  

此渲染流程主要依赖于矩阵变换,我对矩阵变换有些不熟，需要好好研究一番才能在矩阵变换的算法上总结.  
此处，只来描述流程:  
局部坐标系--(世界矩阵)-->世界坐标系--(视图矩阵)-->观察坐标系--(投影矩阵)-->屏幕坐标

世界矩阵包含三种变换:缩放、旋转、平移
观察矩阵包含2种变换:旋转、平移
投影矩阵:根据视锥角、宽高比、远近裁面距离来确定



# 进展

## 2019.1.4
__1.通过矩阵建立起坐标系变换流程,实现平顶、平底，任意三角形三种图元的绘制__  
![Triangle3](https://github.com/SixGodZhang/SREDemo/blob/master/Images/Triangle3.png) 
![Triangle2](https://github.com/SixGodZhang/SREDemo/blob/master/Images/Triangle2.png) 
![Triangle1](https://github.com/SixGodZhang/SREDemo/blob/master/Images/Triangle1.png) 

__2.颜色插值__  
![ColorLearp](https://github.com/SixGodZhang/SREDemo/blob/master/Images/ColorLearp.png)

__3.线框模式__  
![wireframe](https://github.com/SixGodZhang/SREDemo/blob/master/Images/wireframe.png)

## 2019.1.3
__1.使用C#中Window应用窗口的Graphics来模拟C/C++的窗口部分__  
![InitWindow](https://github.com/SixGodZhang/SREDemo/blob/master/Images/201801031530.png) 

__2.画线__  
![slash](https://github.com/SixGodZhang/SREDemo/blob/master/Images/slash.png) 


## 参考资料
C#参考资料: https://www.davrous.com  
C++参考资料: https://github.com/skywind3000/mini3d  
OpenGL: https://learnopengl-cn.github.io/  

参考书籍：  
 - [3D游戏编程大师技巧]
 - [3D数学基础：图形与游戏开发]