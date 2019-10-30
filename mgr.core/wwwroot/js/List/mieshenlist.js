var zTree;
var editTree;
var vm = new Vue({
    el: 'body',
    data: {
        menuTree: [],
        roleAddName: '',
        roleAddDesc: '',
        currentRow: {}
    },
    computed: {},
    ready: function () {
        auth();
    },
    methods: {
        _FreshTable: function () {
            try {
                $(".bootstrap-table button[name='refresh']")[0].click();
            } catch (e) {

            }
        },
        _MieShenEveryEdit: function () {
            var selectRow = $('#MieShenEveryTable').bootstrapTable('getSelections');
            if (selectRow.length < 1) {
                swal({
                    title: "",
                    text: "请选择行！",
                    type: "error"
                });
                return;
            }
            alert(JSON.stringify(selectRow));
        },
    }
});

$(function () {
    $('#bdate').val(getDates());
    $('#MieShenEveryTable')
        .bootstrapTable({
            ajax: ajaxRequest,
            pageNumber: 1,
            pageSize: 10,
            pageList: [10, 20, 25, 50, 100],
            resetOffset: true,
            search: false,
            sortable: true,
            pagination: true,
            height: $(window).height(),
            showRefresh: true,
            showToggle: true,
            showColumns: false,
            striped: true,
            sortName: 'region',
            sortOrder: 'desc',
            clickToSelect: true,
            singleSelect: true,
            cache: false,
            // showPaginationSwitch:true,
            dataType: "json",
            iconSize: 'outline',
            toolbar: '#MieShenEveryTableEventsToolbar',
            icons: {
                refresh: 'glyphicon-repeat',
                toggle: 'glyphicon-list-alt',
                columns: 'glyphicon-list'
            },
            columns: [
                {
                    field: 'state',
                    title: '',
                    checkbox: true
                },
                {
                    field: 'platfrom',
                    title: '平台'
                },
                {
                    field: 'region',
                    title: '区服'
                },
                {
                    field: 'bdate',
                    title: '日期',
                    formatter: function (value, row, index) {
                        return ConvertDates(value)
                    }
                },
                {
                    field: 'BindMoney',
                    title: '绑元'
                },
                {
                    field: 'Money',
                    title: '元宝'
                }
                ,
                {
                    field: 'yesBindMoney',
                    title: '日绑元'
                },
                {
                    field: 'yesMoney',
                    title: '日元宝'
                }
            ],
            onLoadSuccess: function () {

            },
            onToggle: function () {
            },
            onEditableSave: function (field, row, oldValue, $el) {

            }
        });

    $(window).resize(function () {
        $('#MieShenEveryTable').bootstrapTable('resetView', { height: $(window).height() });
    });


    QQT.ajax('/Admin/Region/GetPlatform',
        'POST',
        {
            id: "platform"
        })
        .done(function (response) {
            var searchList = new Array();
            if (response.Rows.length > 0) {
                $.each(response.Rows,
                    function (index, item) {
                        searchList.push({
                            id: item.code,
                            text: item.descript
                        });
                    });

            }

            $('#regionpaltform').select2({
                data: searchList,
                placeholder: '请选择平台',
                multiple: false
            });
        });

});
function ConvertDates(time) {
    //设置当前时间
    var date = new Date(time);
    var year = date.getFullYear();//月份从0~11，所以加一
    var dateArr = [date.getMonth() + 1, date.getDate()];
    for (var i = 0; i < dateArr.length; i++) {
        if (dateArr[i] >= 1 && dateArr[i] <= 9) {
            dateArr[i] = "0" + dateArr[i];
        }
    }
    var strDate = year + '-' + dateArr[0] + '-' + dateArr[1];
    return strDate;
}
function getDates() {
    //设置当前时间
    var date = new Date();
    var year = date.getFullYear();//月份从0~11，所以加一
    var dateArr = [date.getMonth() + 1, date.getDate()];
    for (var i = 0; i < dateArr.length; i++) {
        if (dateArr[i] >= 1 && dateArr[i] <= 9) {
            dateArr[i] = "0" + dateArr[i];
        }
    }
    var strDate = year + '-' + dateArr[0] + '-' + dateArr[1];
    return strDate;
}

function ajaxRequest(params) {
    var pageSize = params.data.limit;
    var pageIndex = params.data.offset / params.data.limit + 1;
    var orderBy = params.data.sort;
    var orderSequence = params.data.order;
    var platformid = $('#regionpaltform').select2('data') && $('#regionpaltform').select2('data').id;
    var region = $('#region').val();
    var bdate = $('#bdate').val();
    QQT.ajax('/Admin/MieShenEvery/GetMieShenEveryList',
        'POST',
        {
            pageIndex: pageIndex,
            pageSize: pageSize,
            orderBy: orderBy,
            orderSequence: orderSequence,
            Platform: platformid,
            Region: region,
            Bdate: bdate,

        })
        .done(function (response) {
            params.success({
                total: response.Total,
                rows: response.Rows
            });
        });

}
