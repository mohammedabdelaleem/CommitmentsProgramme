
$("#profileImageInput").on("change", function (e) {
  const file = e.target.files[0];

  if (file) {
    const reader = new FileReader();
    reader.onload = function (e) {
      $(".avatar-img").attr("src", e.target.result);
    };
    reader.readAsDataURL(file);

    $("#saveProfileImage").removeClass("d-none");
  }
});

$("#saveProfileImage").on("click", function () {

  var file = $("#profileImageInput")[0].files[0];
  var formData = new FormData();

  formData.append("image", file); // bind endpoint with IFormFile image

  var token = $('input[name="__RequestVerificationToken"]').val();
  formData.append("__RequestVerificationToken", token);

  $.ajax({
    url: "/Account/ChangeProfileImage",
    type: "POST",
    data: formData,
    contentType: false,
    processData: false,
    success: function () {
      $("#saveProfileImage").addClass("d-none");
    }
  });
});


$("#profileForm").on("submit", function (e) {
  e.preventDefault();

  const form = $(this);

  const btn = form.find("button");
  btn.prop("disabled", true);

  const data = form.serialize(); // sends all inputs

  $.ajax({
    url: "/Account/UpdateProfileInfo",
    type: "POST",
    data: data,
    success: function (res) {

      btn.prop("disabled", false);

      //  show success message
      $(".save-status")
        .removeClass("d-none")
        .text(res.message)
        .fadeIn()
        .delay(3000)
        .fadeOut();

      //  update UI (optional)
      $(".profile-header p:first").text($("#FullName").val());
      $(".profile-header small").text($("#UserName").val());

    },
    error: function (err) {

      // ❗ handle validation errors
      console.log(err.responseJSON);

      $(".save-status")
        .text("Something went wrong")
        .fadeIn()
        .delay(1500)
        .fadeOut();
    }
  });
});

function clearPasswordErrors(form) {
  form.find("[data-valmsg-for]").text("");
  $("#password-summary-error").text("");

  // remove red borders
  form.find("input").removeClass("is-invalid");
}

$("#passwordForm").on("submit", function (e) {
  e.preventDefault();

  const form = $(this);

  clearPasswordErrors(form);

  $.ajax({
    url: "/Account/ChangeProfilePassword",
    type: "POST",
    data: form.serialize(),

    success: function (res) {

      clearPasswordErrors(form);

      $(".save-pass")
        .removeClass("d-none")
        .text(res.message)
        .fadeIn()
        .delay(3000)
        .fadeOut();

      form.trigger("reset");
    },

    error: function (err) {

      const errors = err.responseJSON || {};

      clearPasswordErrors(form);

      Object.keys(errors).forEach(field => {

        const messages = errors[field];

        if (field === "") {
          // 🔴 General error (Identity)
          $("#password-summary-error").text(messages[0]);
        } else {
          // 🔴 Field error
          const input = form.find(`[name="${field}"]`);

          $(`[data-valmsg-for="${field}"]`)
            .text(messages[0]);

          // 🔥 add red border
          input.addClass("is-invalid");
        }
      });

      // 🔥 focus first invalid input
      const firstErrorField = Object.keys(errors).find(f => f !== "");
      if (firstErrorField) {
        form.find(`[name="${firstErrorField}"]`).focus();
      }
    }
  });
});


// 🔥 clear error while typing
$("#passwordForm input").on("input", function () {
  const input = $(this);
  const name = input.attr("name");

  $(`[data-valmsg-for="${name}"]`).text("");
  $("#password-summary-error").text("");

  input.removeClass("is-invalid");
});


