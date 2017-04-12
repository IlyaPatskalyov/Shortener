import * as React from "react";
import {Navigation} from "../../components/navigation/Navigation";
import {Button, Jumbotron, Input, Col, Row, Grid, Glyphicon, Modal} from "react-bootstrap";
import {LinksApi} from "../../api/LinksApi";

export interface IAddLinkPageState {
    url?: string;
    short?: string;
    error?: string;
}

export class AddLinkPage extends React.Component<{}, IAddLinkPageState> {

    constructor(props: {}, context: {}) {
        super(props, context);
        this.state = {};
    }

    private async addLink() {
        try {
            let short = await LinksApi.add(this.state.url);
            this.setState({short: short});
        }
        catch(e) {
            this.setState({ error: e.message })
        }
    }

    render() {
        return (<div>
            <Navigation/>
            <div className="container body-content">
                <Jumbotron>
                    <h2>Simplify your links</h2>
                    {!this.state.short ?
                        <div>
                            <Input type="text" bsSize="lg" value={this.state.url}
                                   placeholder="Your original URL here"
                                   onChange={e => this.setState({ url: e.target.value })}
                            />
                            <p>
                                <small>All URLs and click analytics are public and can be accessed by anyone</small>
                            </p>
                            <Button bsStyle="primary" bsSize="large"
                                    disabled={!this.state.url || this.state.url.length == 0}
                                    onClick={async e => {
                                        this.addLink();
                                        e.preventDefault();
                                     }}>Shorten URL</Button>
                        </div>
                     :  <div>
                            <h2>{window.location.origin}/{this.state.short}</h2>
                        </div>}
                </Jumbotron>
                <Grid>
                    <Row>
                        <Col lg={4}>
                            <h2><Glyphicon glyph="link"/> Shorten</h2>
                            <p>Shorten your URL so it’s ready to be shared everywhere</p>
                        </Col>
                        <Col lg={4}>
                            <h2><Glyphicon glyph="send"/> Track</h2>
                            <p>Analytics help you know where your clicks are coming from</p>
                        </Col>
                        <Col lg={4}>
                            <h2><Glyphicon glyph="user"/> Learn</h2>
                            <p>Understand and visualize your audience</p>
                        </Col>
                    </Row>
                </Grid>
                <Modal show={this.state.error != null} onHide={() => this.setState({ error: null })}>
                    <Modal.Header closeButton>
                        <Modal.Title>Error</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>{this.state.error}</Modal.Body>
                    <Modal.Footer>
                        <Button onClick={() => this.setState({ error: null })}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        </div>);

    }
}