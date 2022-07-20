// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function confirmDelete(unigueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + unigueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + unigueId;
    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}

//$(document).ready(function () {
//    $('#myTable').DataTable();
//});

$(function () {
    console.log("document ready");
    $(document).on("click", ".edit-product-button", function () {
        console.log("You just clicked button number " + $(this).val());

        //store the product id number
        var productID = $(this).val();
        $.ajax({
            type: 'json',
            data: {
                "id": productID
            },
            url: '/Resume/ShowOneProductJSON',
            success: function (data) {
                console.log(data)

                //fill in the input fields in the modal
                $("#modal-input-id").val(data.id);
                $("#modal-input-name").val(data.name);
                //$("#modal-input-gender-" + data.id)[value = "' + SelectdValue + '"]').attr('checked', true);
                //alert($('input[group="btnradio"][value="' + data.gender + '"]').val());
                //$('#modal-input-gender-' + data.gender).attr('checked', 'checked');
                //$('input[name="btnradio"][value="' + data.gender + '"]').prop('checked', 'false');
                $('input[group="btnradio"][value="' + data.gender + '"]').prop('checked', 'checked');
                //$('input[name="btnradio"][value="' + data.gender + '"]').attr('checked', true);

                //$("#modal-input-gender").val(data.gender);
                $("#modal-input-qualification").val(data.qualification);


                var chkbhtml="";
                $.each(data.genders, function (key, value) {
                    chkbhtml += "<div class='form-check'>"
                    chkbhtml += "<input class='form-check-input' type='checkbox' value='' id='flexCheckDefault'"
                    if (data.gender == value.text) {
                        chkbhtml += " checked=''"
                    }
                    chkbhtml += "> "
                    chkbhtml += "<label class='form-check-label' for='flexCheckDefault'>" + value.text + "</label>"
                    chkbhtml += "</div>"
                    console.log(value.text);
                });

                $("#modal-input-genderlist").html(chkbhtml);

                //$("#modal-input-description").val(data.description);
                //$("#modal-input-status").val(data.status);
                //$("#modal-input-grade").val(data.status);

            }
        })
    });

    $("#save-button").click(function () {
        //get the values from the input fileds and create a json object to submit to the controller.

        var genderValue = "";
        var checkedRadio = $("#div_gender input[type=radio]:checked");
        if (checkedRadio.length > 0) {
            //message += checkedRadio.parent().next().find("label").html();
            genderValue = checkedRadio.val();
        }
        //alert(genderValue);

        var Product = {
            "Id": parseInt($("#modal-input-id").val()),
            "Name": $("#modal-input-name").val(),
            "Gender": genderValue,
            "Age": 40,
            "Qualification": $("#modal-input-qualification").val(),
            "TotalExperience": 3,
            "Language": "C"
            //"Description": $("#modal-input-description").val(),
            //"Status": $("#modal-input-status").val(),
            //"Grade": $("#modal-input-grade").val()
        };

        console.log("save...");
        console.log(Product);

        //save the updated product record in the database using the controller
        $.ajax({
            type: 'json',
            data: Product,
            url: '/Resume/ProcessEditReturnPartial',
            success: function (data) {
                console.log(data);
                $("#card-number-" + Product.Id).html(data).hide().fadeIn(2000);
            }
        })
    })
});

