    var layout = new kendo.Layout('<div id="content"></div>');

    var index = new kendo.View('advertList', { model: indexVM, show: indexVM.show.bind(indexVM), init: indexVM.init.bind(indexVM), hide: indexVM.hide.bind(indexVM) });
    var edit = new kendo.View('editAdvert', { model: editAdvertVM, show: editAdvertVM.show.bind(editAdvertVM), init: editAdvertVM.init.bind(editAdvertVM), hide: editAdvertVM.hide.bind(editAdvertVM) });
    var view = new kendo.View('viewAdvert', { model: viewAdvertVM, show: viewAdvertVM.show.bind(viewAdvertVM), init: viewAdvertVM.init.bind(viewAdvertVM), hide: viewAdvertVM.hide.bind(viewAdvertVM) });

    var router = new kendo.Router();

    router.bind('init', function () {
        layout.render($('#app'));
    });

    router.route('/', function () {
        layout.showIn('#content', index)
    });

    router.route('/edit(/:id)', function (id) {
        editAdvertVM.model.Id = typeof id !== 'undefined' ? id : 0;
        layout.showIn('#content', edit);
    });

    router.route('/view/:id', function (id) {
        viewAdvertVM.model.Id = id;
        layout.showIn('#content', view);
    });

    $(function () {
        router.start();
       // router.navigate('/');
    });

