//增加tabs选项卡  例如：addTab(title,url)
function addTab(title, url) {
    if ($('#tt').tabs('exists', title)) {
        $('#tt').tabs('select', title);
    } else {
        var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
        $('#tt').tabs('add', {
            title: title,
            content: content,
            closable: true
        });
    }
}

var index = 0;
var flag = 1;

//增加tabs选项卡  例如：addtabs('tt')
function addtabs(tabsid) {
    index++;
    $("#" + tabsid).tabs('add', {
        title: 'Tab' + index,
        content: '<div style="padding:10px">Content' + index + '</div>',
        closable: true
    });
}

//移除tabs选项卡   例如：removetabs('tt')
function removetabs(tabsid) {
    var tab = $("#" + tabsid).tabs('getSelected');
    if (tab) {
        var index = $("#" + tabsid).tabs('getTabIndex', tab);
        if (index != 0) {
            $("#" + tabsid).tabs('close', index);
            $("#" + tabsid).tabs('select', index - 1);
            flag = 1;
        }
        else {
            flag = 0;
        }
    }
}

//移除所有tabs选项卡   例如：removeAlltabs('tt')
function removeAlltabs(tabsid) {
    while (flag) {
        var tab = $("#" + tabsid).tabs('getSelected');
        if (tab) {
            var index = $("#" + tabsid).tabs('getTabIndex', tab);
            if (index != 0 && flag == 1) {
                removetabs(tabsid);
            }
            else {
                flag = 0;
            }
        }
    }
    flag = 1;
}

//移除其它tabs选项卡   例如：removeOthertabs('tt')
function removeOthertabs(tabsid) {
    //alert($('#tt').children().eq(0).children().eq(2).children().eq(0).children().length)
    //获得将要关闭的tabs的选项卡总个数
    var tabscount = $("#" + tabsid).children().eq(0).children().eq(2).children().eq(0).children().length;

    var selectedtab = $("#" + tabsid).tabs('getSelected');
    var closeindex = 1; //将tabs的选项卡总个数

    if (selectedtab) {
        var selected = $("#" + tabsid).tabs('getTabIndex', selectedtab);
        for (var index = 1; index < tabscount; index++) {
            if (index != selected) {
                $("#" + tabsid).tabs('close', closeindex);
            }
            else {
                closeindex = closeindex + 1;
            }
        }
        $("#" + tabsid).tabs('select', 1); //将选中的tabs的选项卡打开
    }
}

//根据tabsid移除指定的tabs选项卡   例如：closeAlltabs('tt')
function closeAlltabs(tabsid) {
    //alert($('#tt').children().eq(0).children().eq(2).children().eq(0).children().length);
    $("#" + tabsid).children().eq(0).children().eq(2).children().eq(0).children().each(function (index, obj) {
        var tab = $(".tabs-closable", this).text();
        $(".easyui-tabs").tabs('close', tab);
    });
    $("#close").remove(); //同时把此按钮关闭
}

//只要是样式为.tabs li的所有tabs将会关闭
function closeAll() {
    $(".tabs li").each(function (index, obj) {
        //获取所有可关闭的选项卡
        var tab = $(".tabs-closable", this).text();
        $(".easyui-tabs").tabs('close', tab);
    });
    $("#close").remove(); //同时把此按钮关闭
}

function verificationuesr(k_userid,k_userpassid)
{
	if(k_userid == ""  ){
		alert("用户名不能为空");
		return false;
	}
	if(k_userpassid == ""  ){
		alert("密码不能为空");
		return false;
	}
	else
	{
		alert('check true');
		return true;
	}

	//当checkUser事件写在button按钮里面
	//document.getElementById("formid").submit();
}

function verificationlogin(userid,userpassid) //异步登录验证
{
alert("userid="+userid+"&userpassid="+userpassid);
var xmlhttp;    
 
//1.产生异步请求对象
if (window.XMLHttpRequest){// code for IE7+, Firefox, Chrome, Opera, Safari
  xmlhttp=new XMLHttpRequest();
  }
else{// code for IE6, IE5
  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }

 //2.建立连接
// xmlhttp.open("post","get_verificationuser.php",true);
xmlhttp.open("get","get_verificationuser.php?userid="+userid+"&userpassid="+userpassid,true);

//3.指定回调函数
xmlhttp.onreadystatechange=function()
  {
  if (xmlhttp.readyState==4 && xmlhttp.status==200)
	{
		var xmlresponsetxt=xmlhttp.responseText;
		document.getElementById("txtHint").innerHTML=xmlresponsetxt;
		
		alert('ajax '+xmlresponsetxt);
		//return xmlresponsetxt;
		
		if(xmlresponsetxt=="true")
		{
			alert('ajax true');
			location.href="http://localhost:8080/login/index.html";
			return true;
		}

		document.getElementById("txtHint").innerHTML=xmlresponsetxt;
		/*else
		{
			alert('ajax false');
			return false;
		}*/
	}
  }

//如果以post方式请求，必须要添加 
//xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");  

//4.发送请求,get请求方式时传null  
//xmlhttp.send("userid="+userid+"&userpassid="+userpassid);
xmlhttp.send(null);
}

function showregion(slt_value,next_slt_id) //当前select选中的value值、下一个级联select的id
{
var xmlhttp;    

if (slt_value==""){
  document.getElementById("txtHint").innerHTML="";
  return;
  }
 
if (window.XMLHttpRequest){// code for IE7+, Firefox, Chrome, Opera, Safari
  xmlhttp=new XMLHttpRequest();
  }
else{// code for IE6, IE5
  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }
 
xmlhttp.onreadystatechange=function()
  {
  if (xmlhttp.readyState==4 && xmlhttp.status==200)
	{
		var xmlresponsetxt=xmlhttp.responseText;
		//document.getElementById("txtHint").innerHTML=xmlresponsetxt;
		
		removeAll(next_slt_id);
		addOption(xmlresponsetxt,next_slt_id);
	}
  }
xmlhttp.open("GET","get_region.php?regioncode="+slt_value,true);
xmlhttp.send();
}

function createSelect(){  //动态创建select
	var mySelect = document.createElement("select");  
	mySelect.id = "mySelect";   
	document.body.appendChild(mySelect);  
}  


function addOption(xmlresponsetxt,next_slt_id){   //添加选项option
	if(next_slt_id != null)
	{
		//根据id查找对象，  
		var obj=document.getElementById(next_slt_id);  

		//添加一个选项  
		obj.options.add(new Option("--请选择--","-1")); //这个兼容IE与firefox  
		
		//输出ajax中xmlresponsetxt传过来的文本
		document.getElementById("txtHint").innerHTML=xmlresponsetxt;

		xmlresponsetxt='['+xmlresponsetxt+']';
		xmlresponsetxt=xmlresponsetxt.replace(/\"/g,"'");

		var myobj=eval(xmlresponsetxt);

		//循环添加多个选项
		for(var i=0;i<myobj.length;i++){
			obj.options.add(new Option(myobj[i].meaning,myobj[i].value));
		}
	}
	else
		return;
}  

function removeAll(next_slt_id){     //删除所有选项option
	if (next_slt_id != null)
	{
		var obj=document.getElementById(next_slt_id);  
		obj.options.length=0;
	}
	else
		return;
	  
}  

function removeOne(){   //删除一个选项option
	var obj=document.getElementById('mySelect');  

	//index,要删除选项的序号，这里取当前选中选项的序号  
	var index=obj.selectedIndex;  
	obj.options.remove(index);  
}

function getoptionvalue(){ //获得选项option的文本
	var obj=document.getElementById('mySelect');  

	var index=obj.selectedIndex; //序号，取当前选中选项的序号  
	  
	var val = obj.options[index].value;  
}

function getoptiontext(){ //获得选项option的值
	var obj=document.getElementById('mySelect');  

	var index=obj.selectedIndex; //序号，取当前选中选项的序号  
	  
	var val = obj.options[index].text;  
}

function getoptiontext(){ //修改选项option
	var obj=document.getElementById('mySelect');  

	var index=obj.selectedIndex; //序号，取当前选中选项的序号  
	  
	var val = obj.options[index]=new Option("新文本","新值");  
}

function removeSelect(){  //删除select
	var mySelect = document.getElementById("mySelect");  
	mySelect.parentNode.removeChild(mySelect);  
}  

function show(xmlresponsetxt)  //js中json与数组字符串的相互转化
{
	//第1种js中json与数组字符串的相互转化
	/*var t="{'firstName': 'cyra', 'lastName': 'richardson', 'address': { 'streetAddress': '1 Microsoft way', 'city': 'Redmond', 'state': 'WA', 'postalCode': 98052 },'phoneNumbers': [ '425-777-7777','206-777-7777' ] }"; 
	var jsonobj=eval('('+t+')');
	alert(jsonobj.firstName);
	alert(jsonobj.lastName);*/
	
	//
	//var t2="[{name:'zhangsan',age:'24'},{name:'lisi',age:'30'},{name:'wangwu',age:'16'},{name:'tianqi',age:'7'}] ";

	var t2="[{'value':'110000','meaning':'\u5317\u4eac'},{'value':'360000','meaning':'\u5317\u4eac'}] ";

	var myobj=eval(t2);

	for(var i=0;i<myobj.length;i++){

	   alert(myobj[i].value);

	   alert(myobj[i].meaning);

	}

	//第2种js中json与数组字符串的相互转化
	/*var 
	t2="[{name:'zhangsan',age:'24'},{name:'lisi',age:'30'},{name:'wangwu',age:'16'},{name:'tianqi',age:'7'}] ";
	
	var tt2=xmlresponsetxt.replace(/\"/g,"'"); //将双引号转换为单引号

	document.getElementById("txtHint").innerHTML=tt2;

	var myobj=eval(tt2);

	for(var i=0;i<myobj.length;i++){
	   alert(myobj[i].code);
	}*/

	/*var t3="[['<a href=# 
	onclick=openLink(14113295100,社旗县国税局桥头税务
	所,14113295100,d6d223892dc94f5bb501d4408a68333d,swjg_dm);>14113295100</a>','
	社旗县国税局桥头税务所','社旗县城郊乡长江路西段']]";

	//通过eval() 函数可以将JSON字符串转化为对象

	var obj = eval(t3);

	for(var i=0;i<obj.length;i++){
	   for(var j=0;j<obj[i].length;j++){
			alert(obj[i][j]);
		}

	}*/
}

//自动播放音频文件
function test() {
    $('embed').remove();
    $('body').append('<embed src="../doormove1.wav" autostart="true" hidden="true" loop="false">');
};