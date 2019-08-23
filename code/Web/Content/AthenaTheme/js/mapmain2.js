var canvas, context;
var img,//图片对象
    imgIsLoad,//图片是否加载完成；
    imgX = 0,
    imgY = 0,
    imgScale = 1;

(function int() {
    canvas = document.getElementById('canvas');
    context = canvas.getContext("2d");
    loadImg();
})();

//图片加载
function loadImg() {
    img = new Image();
    img.onload = function () {
        imgIsLoad = true;
        drawImage();
    }
    img.src = "../../images/china_map.jpg"
}

//图片绘制
function drawImage() {
    context.clearRect(0, 0, canvas.width, canvas.height);
    context.drawImage(img, 0, 0, img.width, img.height, imgX, imgY, img.width * imgScale, img.height * imgScale);
}

//图片平移
//html5事件最小细度在DOM上，所以我们无法对canvas上的图像做监听，只能对canvas监听。
//首先监听鼠标mousedown事件，等事件发生之后，再监听鼠标mousemove事件和mouseup事件
//mousemove事件发生之后，获得鼠标移动的位移，相应的图片的位置改变多少
//mouseup事件发生之后，取消对mousemove以及mouseup事件监听
canvas.onmousedown = function (event) {
    alert("onmousedown");
    var pos = windowToCanvas(canvas, event.clientX, event.clientY);
    canvas.onmousemove = function (event) {
        alert("onmousemove");
        canvas.style.cursor = "move";
        var pos1 = windowToCanvas(canvas, event.clientX, event.clientY);
        var x = pos1.x - pos.x;
        var y = pos1.y - pos.y;
        pos = pos1;
        imgX += x;
        imgY += y;
        drawImage();
    }
    canvas.onmouseup = function () {
        alert("onmouseup");
        canvas.onmousemove = null;
        canvas.onmouseup = null;
        canvas.style.cursor = "default";
    }
}

function windowToCanvas(canvas, x, y) {
    var bbox = canvas.getBoundingClientRect();
    return {
        x: x - bbox.left - (bbox.width - canvas.width) / 2,
        y: y - bbox.top - (bbox.height - canvas.height) / 2
    };
}
