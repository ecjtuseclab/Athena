(function($){
var myflow = $.myflow;

$.extend(true,myflow.config.rect,{
	attr : {
	r : 8,
	fill: '#F6F7FF',
	stroke: '#03689A',
	"stroke-width" : 2
}
});

$.extend(true, myflow.config.props.props, {
    id:{name:'id',label:'主键',value:'', editor:function(){return new myflow.editors.inputEditor();}},
    wfname: { name:'wfname', label:'工作流名称', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
    wfmemo: { name:'wfmemo', label:'工作流描述', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
    wfownertable: { name:'wfownertable', label: '业务表', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
    wffieldname: { name:'wffieldname', label:'工作流字段', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
    wfbegintime: { name:'wfbegintime', label:'开始时间', value: '', editor: function () { return new myflow.editors.startTimeEditor(); } },
    wfstoptime: { name:'wfstoptime', label:'结束时间', value: '', editor: function () { return new myflow.editors.endTimeEditor(); } },
    wflock: { name:'wflock', label: '是否锁定', value: '', editor: function () { return new myflow.editors.selectEditor([{ name: '否', value: 1 }, { name: '是', value: 2}]); } }
});
$.extend(true,myflow.config.tools.states,{
			state : {showType: 'text',type : 'state',
				name : {text:'<<state>>'},
				text : {text:'状态'},
				img : {src : 'img/48/task_empty.png',width : 48, height:48},
				props : {
				    text: { name: 'text', label: '显示', value: '', editor: function () { return new myflow.editors.textEditor(); }, value: '状态' },
				    id: { name: 'id', label: '主键', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
					wfnodename: { name: 'wfnodename', label: '节点名称', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
					wfnodememo: { name: 'wfnodememo', label: '节点描述', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
					wfnodelock: { name: 'wfnodelock', label: '是否锁定', value: '', editor: function () { return new myflow.editors.selectEditor([{ name: '否', value: 1 }, { name: '是', value: 2}]); } }
				}},
			task : {showType: 'text',type : 'task',
				name : {text:'<<task>>'},
				text: { text: '任务' },
                attr:{r:50},
				img : {src : 'img/48/task_empty.png',width :48, height:48},
				props : {
					text: {name:'text', label: '显示', value:'', editor: function(){return new myflow.editors.textEditor();}, value:'任务'},
					id: {name:'id', label: '主键', value:'', editor: function(){return new myflow.editors.inputEditor();}},
					nodeactionname: { name: 'nodeactionname', label: '动作名称', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
					nodeactionmemo: { name: 'nodeactionmemo', label: '动作描述', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
					nodetype: { name: 'nodetype', label: '动作类型', value: '', editor: function () { return new myflow.editors.selectEditor([{ name: '普通动作', value: 1 }, { name: '会签动作', value: 2}]); } },
					operatortype: { name: 'operatortype', label: '执行者类型', value: '', editor: function () { return new myflow.editors.selectEditor([{ name: '角色', value: 1 }, { name: '用户', value: 2}]); } },
					nodeoperator: { name: 'nodeoperator', label: '执行者', value: '', editor: function () { return new myflow.editors.inputEditor(); } },
                    operatorlock: { name: 'operatorlock', label: '执行锁定', value: '', editor: function () { return new myflow.editors.selectEditor([{ name: '否', value: 1 }, { name: '是', value: 2}]); } },
				}}
});
})(jQuery);