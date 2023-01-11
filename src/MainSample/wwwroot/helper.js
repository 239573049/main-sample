/**
       * 将图片字节数组转换blob url
       * @param {any} blob 图片字节
       * @param {any} id 添加到 {id}的dom的src特性
       * @return 返回blob地址
       */
function imgToLink(blob, id) {
    var url = (window.URL || window.webkitURL || window || {}).createObjectURL(new Blob([blob], { type: "image/png" }));
    document.getElementById(id).setAttribute("src", url)
    return url
}

/**
 * 设置元素的src
 * @param {any} dest
 * @param {any} srcId
 */
function setImgSrc(dest, srcId, widht, height) {
    let video = document.getElementById("videoFeed");
    let canvas = document.getElementById(dest);
    console.log(video.clientHeight, video.clientWidth);
   
    canvas.getContext('2d').drawImage(video, 0, 0, widht, height);

    canvas.toBlob(function (blob) {
        var src = document.getElementById(srcId);
        src.setAttribute("height", height)
        src.setAttribute("width", widht);
        src.setAttribute("src", URL.createObjectURL(blob))
    }, "image/jpeg", 0.95);
}

/**
 * 释放Blob
 * @param {any} url
 */
function revokeObjectURL(url) {
    URL.revokeObjectURL(url);
}

/**
 * 初始化摄像头
 * @param {any} src 
 */
function startVideo(src) {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            let video = document.getElementById(src);
            if ("srcObject" in video) {
                video.srcObject = stream;
            } else {
                video.src = window.URL.createObjectURL(stream);
            }
            video.onloadedmetadata = function (e) {
                video.play();
            };
            //mirror image
            video.style.webkitTransform = "scaleX(-1)";
            video.style.transform = "scaleX(-1)";
        });
    }
}