function koComponent(ko: KnockoutStatic, component : { name:string, template : any}) {
    return (target: any) => {
        ko.components.register(component.name, {
            viewModel: target,
            template: component.template
        });
        return target;
    }
}
    
