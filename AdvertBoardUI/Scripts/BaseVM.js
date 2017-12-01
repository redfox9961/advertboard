var BaseVM = new function() {
    var model = {};
    model.categories = ko.observableArray([
    {
        categoryID: '100',
        categoryName: 'Все категории',
        hasSubCategory: false
    },
    {
        categoryID: '200',
        categoryName: 'Транспорт',
        hasSubCategory: true,
        subCategories: [
                { subCategoryName: 'Автомобили', categoryID: '201' },
                { subCategoryName: 'Мотоциклы и мототехника', categoryID: '202' },
                { subCategoryName: ' Грузовики и спецтехника ', categoryID: '203' },
                { subCategoryName: 'Водный транспорт', categoryID: '204' },
                { subCategoryName: 'Запчасти и аксессуары', categoryID: '205' }
        ]
    },
    {
        categoryID: '300',
        categoryName: 'Недвижимость ',
        hasSubCategory: true,
        subCategories: [
                { subCategoryName: 'Квартиры ', categoryID: '301' },
                { subCategoryName: 'Комнаты ', categoryID: '302' },
                { subCategoryName: 'Дома, дачи, коттеджи ', categoryID: '303' },
                { subCategoryName: 'Гаражи и машиноместа ', categoryID: '304' },
                { subCategoryName: 'Коммерческая недвижимость ', categoryID: '305' },
                { subCategoryName: 'Недвижимость за рубежом', categoryID: '306' }
        ]
    },
    {
        categoryID: '400',
        categoryName: 'Работа  ',
        hasSubCategory: true,
        subCategories: [
                { subCategoryName: 'Вакансии', categoryID: '401' },
                { subCategoryName: 'Резюме ', categoryID: '402' }
        ]
    }]);

    model.regionsAll = regions;
    model.searchAdvert = function() {
        
    };

    return {
        categories: model.categories,
        regionsAll: model.regionsAll,
        searchAdvert: model.searchAdvert
    }
};