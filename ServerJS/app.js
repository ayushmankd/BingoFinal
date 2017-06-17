var express=require('express');
var app=express();
var bodyparser=require('body-parser');
app.listen(8080);
app.use(bodyparser.urlencoded({ extended: false }));
app.use(bodyparser.json());
var arrDone=new Array();
var lbl=0;
var plyrName;
var plyr=new Array();
var jsonarr=new Array();
var opp;
var first;
var value;
app.post('/Delete/:name',function(req,res)
{
    obj={
        plyrName:plyrName
    }
    obj=req.body;
    var ind=jsonarr.indexOf(obj.plyrName)
    if(ind%2==0)
    {
        for(var i=ind;i<jsonarr.length-2;i++)
        {
            jsonarr[i]=jsonarr[i+2];
        }
    }
    else{
        for(var i=ind;i<jsonarr.length-2;i++)
        {
            jsonarr[i-1]=jsonarr[i+1];
        }
    }
    jsonarr.length=jsonarr.length-2;
    res.end();
})
app.post('/postPlyrName',function(req,res){
    jsonObj={
        plyrName:plyrName
    }
    jsonObj=req.body;
    plyr.push(jsonObj.plyrName);
    obj={
        arrDone:arrDone,
        lbl:lbl,
        plyrName:jsonObj.plyrName
    }
    jsonarr.push(obj);
    console.log('Player ' + jsonObj.plyrName + ' Online');
    res.sendStatus('200');
    res.end();
})
app.get('/opp/:name',function(req,res){
    res.header('200');
    if(jsonarr.length<2)
    {
        obj={
            oppositon:opp
        }
    }
    else if(jsonarr.length%2==0){
        for(var i=0;i<jsonarr.length;i++)
        {
            if(jsonarr[i].plyrName===req.params.name)
            {
                obj={
                    oppositon:opp
                }
                if(i%2==1)
                {
                    obj.oppositon=jsonarr[i-1].plyrName;
                }
                else{
                    obj.oppositon=jsonarr[i+1].plyrName;
                }
            }
        }
    }
    else
    {
        obj={
            oppositon:opp
        }
    }
    res.send(obj);
    res.end();
    console.log('Oppositon Allotted');
})
app.get('/getFirst/:name',function(req,res)
{
    res.header('200');
    for(var i=0;i<jsonarr.length;i++)
    {
        if(req.params.name===jsonarr[i].plyrName)
        {
            obj={
                first:first
            }
            if((i%2)===0)
            {
                obj.first=1;
            }
            else if(i%2==1)
            {
                obj.first=0;
            }
            break;
        }
    }
    res.send(obj);
    res.end();
})
app.get('/get/:name',function(req,res){
    res.header('200');
    for(var i=0;i<jsonarr.length;i++)
    {
        if(req.params.name===jsonarr[i].plyrName)
        {
            obj={
            arrDone:jsonarr[i].arrDone,
            lbl:l=jsonarr[i].lbl
            }
            break;
        }   
    }
    res.send(obj);
    console.log('Request by' + req.params.name );
    res.end();
})
app.post('/post/:name',function(req,res){
    obj={
        arrDone:arrDone,
        lbl:lbl
    }
    obj=req.body;
    for(var i=0;i<jsonarr.length;i++)
    {
        if(jsonarr[i].plyrName===req.params.name)
        {
            jsonarr[i].arrDone=obj.arrDone;
            jsonarr[i].lbl=obj.lbl;
        }
    }
    console.log('Posted by '+req.params.name);
    res.end();
})
console.log('Server started');