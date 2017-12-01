var editAdvertVM = kendo.observable({
    pageConfig: {
        id: 'editAdvertisement'
    },
    model: {
        Id: 0,
        Title: '',
        Text: '',
        PublishDate: '',
        Price: '' 
    },
    init: function () {
        $('#' + this.pageConfig.id + ' .datepicker').kendoDatePicker();
    },
    show: function () {

    },
    hide: function () {
        this.model.set('Id', 0);
        this.model.set('Title', '');
        this.model.set('Text','');
        this.model.set('PublishDate','');
        this.model.set('Price', ''); 
    },
    saveAdvert: function () {
        var self = this;
        $.ajax({
            url: '/api/adverts',
            type: 'POST',
            data: JSON.parse(JSON.stringify(self.model)),
            success: function () {
                debugger;
            },
            error: function () {
                debugger;
            }
        })
    },
    cancelEdit: function () {
        router.navigate('/');
    }
});