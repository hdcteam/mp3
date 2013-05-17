$(document).ready(function () {
    $(".tools .like-btn").click(function () {
        if (this.innerText == "Thích")
            this.innerText = "Bỏ thích";
        else
            this.innerText = "Thích";
    });
});