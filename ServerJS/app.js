var express=require('express');
var app=express();
var bodyparser=require('body-parser');
app.listen(8080);
app.use(bodyparser.urlencoded({ extended: false }));
app.use(bodyparser.json());


var arrDone=new Array();
var lbl=0;
var plyrName;
var jsonarr=new Array();
var opp;
var first;
var value;


function CheckVal(value)
{
    for(var i=0;i<jsonarr.length;i++)
    {
        if(jsonarr[i].plyrName==value)
        {
            return 0;
        }
    }
    return 1;
}

app.post('/postPlyrName',function(req,res){
    jsonObj={
        plyrName:plyrName
    }
    jsonObj=req.body;
    if(CheckVal(jsonObj.plyrName))
    {
        obj={
            arrDone:arrDone,
            lbl:lbl,
            plyrName:jsonObj.plyrName
        }
        jsonarr.push(obj);
        console.log('Player ' + jsonObj.plyrName + ' Online');
        res.sendStatus(200);
    }
    else
    {
        res.sendStatus(404);
    }
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


app.get('/getFirst/:name',function(req,res)
{
    if(CheckVal(req.params.name))
    { 
        res.sendStatus(404);
        res.end();
    }
    else
    {  
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
                else
                {
                    obj.first=0;
                }
                break;
            }
        }
    res.status(200).send(obj);
    res.end();
    }
})


app.get('/opp/:name',function(req,res){
    if(CheckVal(req.params.name))
    {
        res.sendStatus(404);
        res.end();
    }
    else if(jsonarr.length<2)
    {
        res.sendStatus(303);
        res.end();
    }
    else if(jsonarr.length%2==0)
    {
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
        res.status(200).send(obj);
        res.end();
    }
    else
    {
        res.sendStatus(303);
        res.end();
    }
    res.end();
    console.log('Oppositon Allotted');
})


app.get('/get/:name',function(req,res){
    if(CheckVal(req.params.name))
    { 
        res.sendStatus(404);
        res.end();
    }
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
    res.status(200).send(obj);
    console.log('Request by' + req.params.name );
    res.end();
})

app.post('/Delete/:name',function(req,res)
{
    obj={
        plyrName:plyrName
    }
    obj=req.body;
    var ind;
    for(var i=0;i<jsonarr.length;i++)
    {
        if(jsonarr[i].plyrName==obj.plyrName)
            ind=i;
    }
    if(jsonarr.length%2!=0)
    {
        jsonarr.pop();
    }
    else if(ind+1==jsonarr.length || ind+2==jsonarr.length)
    {
        jsonarr.pop();
        jsonarr.pop();
    }
    else
    {
        if(ind%2==0)
        {
            for(var i=ind;i<jsonarr.length-2;i++)
            {
                jsonarr[i].plyrName=jsonarr[i+2].plyrName;
                jsonarr[i].arrDone=jsonarr[i+2].arrDone;
                jsonarr[i].lbl=jsonarr[i+2].lbl;
                jsonarr.pop();
                jsonarr.pop();
            }
        }
        else
        {
            for(var i=ind-1;i<jsonarr.length-2;i++)
            {
                jsonarr[i].plyrName=jsonarr[i+2].plyrName;
                jsonarr[i].arrDone=jsonarr[i+2].arrDone;
                jsonarr[i].lbl=jsonarr[i+2].lbl;
                jsonarr.pop();
                jsonarr.pop();
            }
        }
    }
    res.sendStatus(200);
    res.end();
})
console.log('Server started');