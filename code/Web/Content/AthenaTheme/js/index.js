var s = Snap(document.getElementById("clock"));

var seconds = s.select("#seconds"),
    minutes = s.select("#minutes"),
    hours   = s.select("#hours"),
    rim     = s.select("#rim"), 
    face    = {
      elem: s.select("#face"),
      cx: s.select("#face").getBBox().cx,
      cy: s.select("#face").getBBox().cy,
    },
    angle   = 0,
    easing = function(a) {
      return a==!!a?a:Math.pow(4,-10*a)*Math.sin((a-.075)*2*Math.PI/.3)+1;
    };

function update() {
  var time = new Date();
  setHours(time);
  setMinutes(time);
  setSeconds(time);
}

function setHours(t) {
  var hour = t.getHours();
  hour %= 12;
  hour += Math.floor(t.getMinutes()/10)/6;
  var angle = hour*360/12;
  hours.animate(
    {transform: "rotate("+angle+" 244 251)"},
    100,
    mina.linear
  );
}
function setMinutes(t) {
  var minute = t.getMinutes();
  minute %= 60;
  minute += Math.floor(t.getSeconds()/10)/6;
  var angle = minute*360/60;
  minutes.animate(
    {transform: "rotate("+angle+" "+face.cx+" "+face.cy+")"},
    100,
    mina.linear
  );
}
function setSeconds(t) {
  t = t.getSeconds();
  t %= 60;
  var angle = t*360/60;
  //if ticking over to 0 seconds, animate angle to 360 and then switch angle to 0
  if (angle === 0) angle = 360;
  seconds.animate(
    {transform: "rotate("+angle+" "+face.cx+" "+face.cy+")"},
    600,
    easing,
    function(){
      if (angle === 360){
        seconds.attr({
          transform: "rotate("+0+" "+face.cx+" "+face.cy+")",
         })
      }
    }
  );
}
console.log(face);
setInterval(update, 1000);