import * as React from "react";
import {Navigation} from "../../components/navigation/Navigation";
import {Link, LinksApi} from "../../api/LinksApi";
import {Table} from "react-bootstrap";

export class MyLinksPage extends React.Component<{}, {myLinks?: Link[]}> {

    constructor(props: {}, context: {}) {
        super(props, context)
        this.state = {};
    }

    componentWillMount() {
        this.load();
    }

    async load() {
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
                                <td><a target="_blank" href={`/${l.Key}`}>{l.Key}</a></td>
                                <td><a target="_blank" href={l.Url}>{l.Url}</a></td>
                                <td>{l.Created}</td>
                                <td>{l.CountOfRedirects}</td>
                            </tr>)}
                        </tbody>
                    </Table>}
            </div>
        </div>);

    }
}