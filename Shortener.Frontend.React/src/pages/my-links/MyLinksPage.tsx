import * as React from "react";
import {Navigation} from "../../components/navigation/Navigation";
import {LinksApi} from "../../api/LinksApi";
import {Table} from "react-bootstrap";
import moment = require("moment");

export class MyLinksPage extends React.Component<{}, {myLinks?: Link[]}> {

    constructor(props: {}, context: {}) {
        super(props, context);
        this.state = {};
    }

    componentWillMount() {
        this.load();
    }

    private async load() {
        let links = await LinksApi.getMyLinks();
        this.setState({myLinks: links});
    }

    render() {

        return (<div>
            <Navigation/>
            <div className="container body-content">
                <h1>My links</h1>

                {!this.state.myLinks ?
                    "Loading... "
                    :
                    <Table striped bordered condensed hover>
                        <thead>
                        <tr>
                            <th>Short link</th>
                            <th>Link</th>
                            <th>Created</th>
                            <th>Count of redirects</th>
                        </tr>
                        </thead>
                        <tbody>
                        {this.state.myLinks.map(l =>
                            <tr>
                                <td><a target="_blank" href={`/${l.key}`}>{l.key}</a></td>
                                <td><a target="_blank" href={l.url}>{l.url.length > 100 ? l.url.substr(0, 100) + "..." : l.url}</a></td>
                                <td>{moment(l.created).format("DD.MM.YYYY hh:mm")}</td>
                                <td>{l.countOfRedirects}</td>
                            </tr>)}
                        </tbody>
                    </Table>}
            </div>
        </div>);

    }
}