
function confirmDelete({
    itemId,
    url,
    removeCallback,
    successText = "تم حذف العنصر بنجاح"
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
        title: " هل انت متاكد من الحذف؟",
        text: "لن تتمكن من التراجع عن هذا",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "نعم!",
        cancelButtonText: "تراجع",
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

                    toastr.success(res.message, "تمت عملية الحذف", {
                        timeOut: 3000,
                        closeButton: true,
                        progressBar: true
                    });

                } else {
                    toastr.error(res.message);
                }
            },

            error: function () {
                toastr.error(res.message);
            }
        });

    });
}

//for TABLE(Admin)
function deleteItem(itemId, controller) {
    console.log(`item id : ${itemId} ==> ${typeof itemId} `)
    confirmDelete({
        itemId: itemId,
        url: `/Admin/${controller}/Delete`,

        removeCallback: function (id) {
            $(`tr[data-id='${id}']`).remove();
        }
    });
}

