$(document).ready(function () {

    $(".new-comment form").submit(function () {
        this.submit.disabled = true;
        this.submit.value = "Đang tải ...";
        var form = this;
        $.ajax(
            {
                url: '@Url.Action("Create", "SongComment")', 
                data: {songId: _songId, content: this.cmtContent.value}, 
                success: function (data) {

                    if (data.type == "SUCCESS")
                    {
                        $(".new-comment").hide();
                        $(".comment-list").append(data.html);
                        $(".no-comment").hide();
                    }
                    else
                    {
                        alert("Lỗi" + data.msg);
                        form.submit.disabled = false;
                        form.submit.value = "Đăng bình luận";
                    }
                },

                error: function(jqXHR, textStatus, errorThrown) {
                    alert(textStatus + ":" + jqXHR.responseText );
                    form.submit.disabled = false;
                    form.submit.value = "Đăng bình luận";
                },

                dataType: "json",
                type: "POST"
            });
        return false;
    });
});