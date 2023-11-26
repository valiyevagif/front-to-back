(function(l,r,i){"use strict";l.fn.scrollUp=function(o){l.data(i.body,"scrollUp")||(l.data(i.body,"scrollUp",!0),l.fn.scrollUp.init(o))},l.fn.scrollUp.init=function(o){var e=l.fn.scrollUp.settings=l.extend({},l.fn.scrollUp.defaults,o),a=!1,c,n,t,p,d,f,s;switch(e.scrollTrigger?s=l(e.scrollTrigger):s=l("<a/>",{id:e.scrollName,href:"#top"}),e.scrollTitle&&s.attr("title",e.scrollTitle),s.appendTo("body"),e.scrollImg||e.scrollTrigger||s.html(e.scrollText),s.css({display:"none",position:"fixed",zIndex:e.zIndex}),e.activeOverlay&&l("<div/>",{id:e.scrollName+"-active"}).css({position:"absolute",top:e.scrollDistance+"px",width:"100%",borderTop:"1px dotted"+e.activeOverlay,zIndex:e.zIndex}).appendTo("body"),e.animation){case"fade":c="fadeIn",n="fadeOut",t=e.animationSpeed;break;case"slide":c="slideDown",n="slideUp",t=e.animationSpeed;break;default:c="show",n="hide",t=0}e.scrollFrom==="top"?p=e.scrollDistance:p=l(i).height()-l(r).height()-e.scrollDistance,d=l(r).scroll(function(){l(r).scrollTop()>p?a||(s[c](t),a=!0):a&&(s[n](t),a=!1)}),e.scrollTarget?typeof e.scrollTarget=="number"?f=e.scrollTarget:typeof e.scrollTarget=="string"&&(f=Math.floor(l(e.scrollTarget).offset().top)):f=0,s.click(function(g){g.preventDefault(),l("html, body").animate({scrollTop:f},e.scrollSpeed,e.easingType)})},l.fn.scrollUp.defaults={scrollName:"scrollUp",scrollDistance:300,scrollFrom:"top",scrollSpeed:300,easingType:"linear",animation:"fade",animationSpeed:200,scrollTrigger:!1,scrollTarget:!1,scrollText:"Scroll to top",scrollTitle:!1,scrollImg:!1,activeOverlay:!1,zIndex:2147483647},l.fn.scrollUp.destroy=function(o){l.removeData(i.body,"scrollUp"),l("#"+l.fn.scrollUp.settings.scrollName).remove(),l("#"+l.fn.scrollUp.settings.scrollName+"-active").remove(),l.fn.jquery.split(".")[1]>=7?l(r).off("scroll",o):l(r).unbind("scroll",o)},l.scrollUp=l.fn.scrollUp})(jQuery,window,document);
//# sourceMappingURL=/s/files/1/0019/7181/4451/t/10/assets/jquery.scrollUp.js.map?v=95159205425930492741632031669
