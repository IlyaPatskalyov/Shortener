
interface FlatironRouter
{
	//new(routes: any);
    init(r? : string) : FlatironRouter;
    explode() : string[];
    setRoute(route : string) : void;
    setRoute(start: number, length : number) : void;
    setRoute(index : number, value : string) : void;
    getRoute(index? : number) : string;
    configure(options : FlatironRouterConfiguration) : FlatironRouter;
    param(token : string, matcher : string) : void;
    param(token : string, matcher : any) : void;
    on(method : string, path : string, route : Function) : void;
    on(method : string, path : string, route : Function[]) : void;
    route(method : string, path : string, route : Function) : void;
    route(method : string, path : string, route : Function[]) : void;
    dispatch(method : string, path : string, callback?: Function) : void;
    mount(routes : Object, path : string) : void;
    mount(routes : Object, path : any) : void;
    path(path : string, routesFn : Function) : void;
    path(path : any, routesFn : Function) : void;
}


interface FlatironRouterConfiguration
{
    recurse?: any;
    strict?: boolean;
    async?: boolean;
    delimiter?: string;
    notfound?: Function;
    on?: Function[];
    before?: Function[];
    resource?: Object;
    after?: Function[];
    html5history?: boolean;
    run_handler_in_init?: boolean;
}

declare module "director" {
    
    class Router implements FlatironRouter {
        constructor(routes: any);
        init(r?: string): FlatironRouter;
        explode(): string[];
        setRoute(route: string): void;
        setRoute(start: number, length: number): void;
        setRoute(index: number, value: string): void;
        getRoute(index?: number): string;
        configure(options: FlatironRouterConfiguration): FlatironRouter;
        param(token: string, matcher: string): void;
        param(token: string, matcher: any): void;
        on(method: string, path: string, route: Function): void;
        on(method: string, path: string, route: Function[]): void;
        route(method: string, path: string, route: Function): void;
        route(method: string, path: string, route: Function[]): void;
        dispatch(method: string, path: string, callback?: Function): void;
        mount(routes: Object, path: string): void;
        mount(routes: Object, path: any): void;
        path(path: string, routesFn: Function): void;
        path(path: any, routesFn: Function): void;
    }

    export = Router

}
