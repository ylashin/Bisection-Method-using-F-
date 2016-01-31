(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,Html,Client,Operators,List,Attr,Tags,T,Concurrency,Remoting,AjaxRemotingProvider,EventsPervasives;
 Runtime.Define(Global,{
  BisectionWeb:{
   Client:{
    Main:function()
    {
     var input,arg10,hdResult,arg101,x,output,arg102,arg103,x1,arg00,arg104;
     arg10=List.ofArray([Attr.Attr().NewAttr("value",""),Attr.Attr().NewAttr("id","txtEquation")]);
     input=Operators.add(Tags.Tags().NewTag("input",arg10),Runtime.New(T,{
      $:0
     }));
     arg101=List.ofArray([Attr.Attr().NewAttr("value",""),Attr.Attr().NewAttr("id","hdResult"),Attr.Attr().NewAttr("type","hidden")]);
     hdResult=Operators.add(Tags.Tags().NewTag("input",arg101),Runtime.New(T,{
      $:0
     }));
     input["HtmlProvider@33"].SetCss(input.get_Body(),"width","300px");
     x=Runtime.New(T,{
      $:0
     });
     output=Tags.Tags().NewTag("h4",x);
     arg103=List.ofArray([Tags.Tags().text("Solve and Plot")]);
     x1=Tags.Tags().NewTag("button",arg103);
     arg00=function()
     {
      return function()
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        return Concurrency.Bind(AjaxRemotingProvider.Async("BisectionWeb:1",[input.get_Value(),1]),function(_arg11)
        {
         hdResult.set_Value(_arg11);
         return Concurrency.Return(null);
        });
       }),{
        $:0
       });
      };
     };
     EventsPervasives.Events().OnClick(arg00,x1);
     arg104=Runtime.New(T,{
      $:0
     });
     arg102=List.ofArray([input,hdResult,x1,Tags.Tags().NewTag("hr",arg104)]);
     return Tags.Tags().NewTag("div",arg102);
    },
    Start:function(input,valx,k)
    {
     var arg00;
     arg00=Concurrency.Delay(function()
     {
      return Concurrency.Bind(AjaxRemotingProvider.Async("BisectionWeb:1",[input,valx]),function(_arg1)
      {
       return Concurrency.Return(k(_arg1));
      });
     });
     return Concurrency.Start(arg00,{
      $:0
     });
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  Html=Runtime.Safe(Global.WebSharper.Html);
  Client=Runtime.Safe(Html.Client);
  Operators=Runtime.Safe(Client.Operators);
  List=Runtime.Safe(Global.WebSharper.List);
  Attr=Runtime.Safe(Client.Attr);
  Tags=Runtime.Safe(Client.Tags);
  T=Runtime.Safe(List.T);
  Concurrency=Runtime.Safe(Global.WebSharper.Concurrency);
  Remoting=Runtime.Safe(Global.WebSharper.Remoting);
  AjaxRemotingProvider=Runtime.Safe(Remoting.AjaxRemotingProvider);
  return EventsPervasives=Runtime.Safe(Client.EventsPervasives);
 });
 Runtime.OnLoad(function()
 {
  return;
 });
}());
