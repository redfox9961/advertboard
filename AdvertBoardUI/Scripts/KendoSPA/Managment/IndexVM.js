var indexVM = kendo.observable({
    pageConfig: {
        id: 'userAdvertisementsBoard'
    },
    model: {},
    filter: {
        title: '',
        text: '',
        startDate: '',
        endDate: '',
        startPrice: '',
        endPrice: ''
    },
    init: function () {
        $('#' + this.pageConfig.id + ' .datepicker').kendoDatePicker({});
        $('#' + this.pageConfig.id + ' #advertlistView').kendoListView({
            template: kendo.template($("#advert-item").html()),
            dataSource: {
                transport: {
                    read: {
                        url: '/api/adverts/'
                    }
                }
            },
            autoBind: false
        });

    },
    show: function () {
        $('#' + this.pageConfig.id + ' #advertlistView').data('kendoListView').dataSource.read();
    },
    hide: function () {
        console.log('hide');
    },
    searchAdverts: function () {
        console.log(this.filter);
    },
    clearFilter: function () {
        this.filter.set('title', '');
        this.filter.set('text', '');
        this.filter.set('startDate', '');
        this.filter.set('endDate', '');
        this.filter.set('startPrice', '');
        this.filter.set('endPrice', '');
    },
    addAdvert: function () {
        router.navigate('/edit');
    }
});