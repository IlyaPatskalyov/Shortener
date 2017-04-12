import "text!./Navigation.html";
import * as ko from "knockout";
export = NavigationComponent

@koComponent(ko, {
    name: "navigation",
    template: require('text!./Navigation.html')
})
class NavigationComponent {

    constructor(params: {  }) {

    }
}

