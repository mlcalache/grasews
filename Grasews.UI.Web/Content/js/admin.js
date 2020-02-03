$(function () {

    $(".select2").select2();

    $(".select2-multiple").select2({
        allowClear: true,
        tags: true,
        placeholder: ""
    });
});