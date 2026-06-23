function confirmDelete({
    itemId,
    url,
    removeCallback,
    successText = "Item deleted"
}) {

    const isDarkMode = false;
    //    document.body.classList.contains("dark-mode") ||
    //    localStorage.getItem("theme") === "dark" ||
    //    window.matchMedia("(prefers-color-scheme: dark)").matches;

    const swalTheme = {
        background: isDarkMode ? "#1e1e2f" : "#ffffff",
        textColor: isDarkMode ? "#dcdcdc" : "#333",
        confirmButtonColor: isDarkMode ? "#4a90e2" : "#3085d6",
        cancelButtonColor: isDarkMode ? "#e74c3c" : "#d33",
    };

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        background: swalTheme.background,
        color: swalTheme.textColor,
        confirmButtonColor: swalTheme.confirmButtonColor,
        cancelButtonColor: swalTheme.cancelButtonColor
    }).then((result) => {

        if (!result.isConfirmed) return;

        $.ajax({
            url: url,
            type: "POST",
            data: {
                id: itemId,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },

            success: function (res) {

                if (res.success) {
                    //$(".dropdown-menu").removeClass("show");
                    if (removeCallback) removeCallback(itemId);

                    toastr.error(res.message, "Deleted", {
                        timeOut: 3000,
                        closeButton: true,
                        progressBar: true
                    });

                } else {
                    toastr.error(res.message);
                }
            },

            error: function () {
                toastr.error("Something went wrong");
            }
        });

    });
}

//for TABLE(Admin)
function deleteItem(itemId, controller) {
    confirmDelete({
        itemId: itemId,
        url: `/Admin/${controller}/Delete`,

        removeCallback: function (id) {
            $(`tr[data-id='${id}']`).remove();
        }
    });
}

function deletePost(postId) {
    confirmDelete({
        itemId: postId,
      url: "/Post/RemoveHidePost",

        removeCallback: function (id) {
            $(`#post-${id}`).fadeOut(300, function () {
                $(this).remove();
            });

            bootstrap.Modal.getInstance(
                document.getElementById("postDetailsModal")
            )?.hide();

        },

        successText: "Post deleted"
    });
}