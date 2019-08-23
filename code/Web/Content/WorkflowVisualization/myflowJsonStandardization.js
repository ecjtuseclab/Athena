function JsonStandardization(jsonStr)
{
    
    //初始化工作流
    var obj = eval('(' + jsonStr + ')');
    console.log(obj);
    var workflowPanel = new WorkflowPanel(
             obj.props.props.id.value,
             obj.props.props.wfname.value,
             obj.props.props.wfmemo.value,
             obj.props.props.wfownertable.value,
             obj.props.props.wffieldname.value,
             obj.props.props.wfbegintime.value,
             obj.props.props.wfstoptime.value,
             obj.props.props.wfstatus.value,
             obj.props.props.wflock.value
            );
    //添加节点信息
    for (var pKey in obj.paths)
    {
        var path = new Path(pKey, obj.paths[pKey].from, obj.paths[pKey].to);
        workflowPanel.Paths.push(path);
    }
    for (var sKey in obj.states) {
        if (obj.states[sKey].type == "state") {
            var node = new Node(
                    obj.states[sKey].props.id.value,
                    sKey,
                    obj.states[sKey].props.wfnodename.value,
                    obj.states[sKey].props.wfnodememo.value,
                    obj.states[sKey].props.wfnodelock.value,
                    obj.states[sKey].props.wfnodestatus.value,
                    obj.states[sKey].type
                    );
            workflowPanel.Nodes.push(node);
        }
        else if (obj.states[sKey].type == "task")
        {
            opid = '<op' + sKey + '>';
            var nodeaction = new NodeAction(
                     obj.states[sKey].props.id.value,
                     sKey,
                     obj.states[sKey].props.actionid.value,
                     obj.states[sKey].props.nastatus.value,
                     obj.states[sKey].props.nalock.value,
                     obj.states[sKey].props.nodetype.value,
                     obj.states[sKey].props.opid.value,
                     obj.states[sKey].props.operatortype.value,
                     obj.states[sKey].props.nodeoperator.value,
                     obj.states[sKey].props.operatorstatus.value,
                     obj.states[sKey].props.operatorlock.value,
                     obj.states[sKey].type
                    );
            obj.states[sKey].props.opid.value = opid;
            workflowPanel.NodeActions.push(nodeaction);
        }

        obj.states[sKey].props.id.value = '<' + sKey + '>';
    }
    var result = [];
    console.log(workflowPanel);
    console.log(obj);
    result.push(JSON.stringify(workflowPanel));
    result.push(JSON.stringify(obj));
    return result;
}

function WorkflowPanel(id, name, memo, ownertable, fieldname, begintime, stoptime, wfstatus, lock) {
    this.id = id;
    this.wfname = name;
    this.wfmemo = memo;
    this.wfownertable = ownertable;
    this.wffieldname = fieldname;
    this.wfbegintime = begintime;
    this.wfstoptime = stoptime;
    this.wfstatus = wfstatus;
    this.wflock = lock;
    this.Paths = [];
    this.Nodes = [];
    this.NodeActions = [];
}
function Path(pid, from, to) {
    this.pathID = pid;
    this.from = from;
    this.to = to;
}
function Node(id, StateID, wfnname, wfnmemo, wfnlock, wfntatus, typen) {
    this.id = id;
    this.StateID = StateID;
    this.wfnodename = wfnname;
    this.wfnodememo = wfnmemo;
    this.wfnodelock = wfnlock;
    this.wfnodestatus = wfntatus;
    this.typeName = typen;
}
function NodeAction(id, stateID, actionid, nastatus, nalock, ntype, opid, otype,nodeoperator, operatorstatus, olock, typen) {
    this.id = id;
    this.StateID = stateID;
    this.actionid = actionid;
    this.nastatus = nastatus;
    this.nalock = nalock;
    this.nodetype = ntype;
    this.opid = opid;
    this.operatortype = otype;
    this.nodeoperator = nodeoperator;
    this.operatorstatus = operatorstatus;
    this.operatorlock = olock;
    this.typeName = typen;

}