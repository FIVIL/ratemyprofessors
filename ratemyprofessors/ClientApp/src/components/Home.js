import React, { Component } from 'react';
import { Col, Grid, Row, Glyphicon } from 'react-bootstrap';
import history from './history'
import Rating from 'react-rating'
import { Footer } from './Footer'
export class Home extends Component {
    displayName = Home.name
    constructor(props) {
        super(props);
        this.state = {
            Bests: [], Worsts: [], Latests: []
        };
        this.FetchData();
    }
    FetchData() {
        fetch('api/Comments/latest')
            .then(response => response.json())
            .then(data => {
                this.setState({ Latests: data });
            }).catch(r => console.log(r));
        fetch('api/Professors/GetBests')
            .then(response => response.json())
            .then(data => {
                this.setState({ Bests: data });
            }).catch(r => console.log(r));
        fetch('api/Professors/GetWorst')
            .then(response => response.json())
            .then(data => {
                this.setState({ Worsts: data });
            }).catch(r => console.log(r));
    }
    static RenderBest(Best) {
        return (
            <div>
                {Best.map(p =>
                    <div className="ProfAndCourse BluerAdminHr" onClick={() => history.push('/Professor/' + p.id)}>
                        <div className="ProfAndCourseInnerHome">
                            <div style={{ width: '20%', float: 'right' }}>
                                <img alt={p.fullName}
                                    src={p.imageLink == 'nopic' ? require('../imgs/nopic.png') : p.imageLink}
                                    style={{ width: '99%', height: '85px' }} />
                            </div>
                            <div style={{ width: '75%', float: 'left' }}>
                                <h3 className="InnerText">
                                    <text className="InnerRight">{p.fullName}</text>
                                    <div className="InnerLeft">
                                        {
                                            (p.score > 0) ?
                                                <div>                                                
                                                    <Rating
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star GGold"
                                                        readonly
                                                        initialRating={p.score}
                                                    />
                                                    <small className="text-success"> {p.score} </small>
                                                </div>
                                                :
                                                <div>
                                                    <Rating
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        readonly
                                                        initialRating={p.score * -1}
                                                        className="NegetiveRate"
                                                    />
                                                    <small className="text-danger"> {p.score} </small>
                                                </div>
                                        }
                                    </div>
                                </h3>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        );
    }
    static RenderWorst(Worst) {
        return (
            <div>
                {Worst.map(p =>
                    <div className="ProfAndCourse BluerAdminHr" onClick={() => history.push('/Professor/' + p.id)}>
                        <div className="ProfAndCourseInnerHome">
                            <div style={{ width: '20%', float: 'right' }}>
                                <img alt={p.fullName}
                                    src={p.imageLink == 'nopic' ? require('../imgs/nopic.png') : p.imageLink}
                                    style={{ width: '99%', height: '85px' }} />
                            </div>
                            <div style={{ width: '75%', float: 'left' }}>
                                <h3 className="InnerText">
                                    <text className="InnerRight">{p.fullName}</text>
                                    <div className="InnerLeft">
                                        {
                                            (p.score > 0) ?
                                                <div>
                                                    <Rating
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star GGold"
                                                        readonly
                                                        initialRating={p.score}
                                                    />
                                                    <small className="text-success"> {p.score} </small>
                                                </div>
                                                :
                                                <div>
                                                    <Rating
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        readonly
                                                        initialRating={p.score * -1}
                                                        className="NegetiveRate"
                                                    />
                                                    <small className="text-danger"> {p.score} </small>
                                                </div>
                                        }
                                    </div>
                                </h3>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        );
    }
    render() {
        let Best = Home.RenderBest(this.state.Bests);
        let Worst = Home.RenderWorst(this.state.Worsts);
        return (
            <div>
                <div className="HomeBackgroundImage"></div>
                <div className="HomeContent">
                    <br />
                    <br />
                    <br />
                    <h1 className="HeaderTextt">اگه قرار بود به استاد هات نمره بدی، چند میدادی؟؟</h1>
                    <div className="container">
                        <Grid fluid>
                            <Row>
                                <Col md={12}>
                                    <div className="MainColsText">
                                        <p>
                                            <strong className="text-justify">
                                                برای امتیاز دادن نام استاد مورد نظر خود را از منوی بالا جستجو کنید و یا به صفحه دانشکده خود مراجعه کنید
                                                        .
                       </strong> </p>
                                        <p>
                                            <strong>
                                                برای انتخاب بهترین استاد هر درس در زمان انتخاب واحد می توانید نام درس مورد نظر خود را
                             جستجو کنید.
                   </strong>     </p>
                                    </div>
                                </Col>
                                <Col md={6}>
                                    <div className="MainCols">
                                        <div className="BluerAdminHr">
                                            <h3 className="text-danger">
                                                <Glyphicon glyph='thumbs-down' style={{ color: '#3B5998 ', float: 'left' }} />
                                                <text style={{ float: 'right' }}>نیازمند تلاش بیشتر:/</text>
                                            </h3>
                                            <br />
                                            <br />
                                        </div>
                                        {Best}
                                    </div>
                                </Col>
                                {/*<Col md={4}>
                                <div className="MainCols">
                                    <div className="BluerAdminHr">
                                        <h3>
                                            <Glyphicon glyph='triangle-top' style={{ color: '#C145FF ', float: 'left' }} />
                                            <text style={{ float: 'right' }}>جدید ترین نظرات</text>
                                        </h3>
                                        <br />
                                        <br />
                                    </div>
                                    <br />
                                </div>
                            </Col>*/}
                                <Col md={6}>
                                    <div className="MainCols">
                                        <div className="BluerAdminHr">
                                            <h3 className="MySuccess">
                                                <Glyphicon glyph='heart' style={{ color: '#e31b23', float: 'left' }} />
                                                <text style={{ float: 'right' }}>محبوب ترین ها</text>
                                            </h3>
                                            <br />
                                            <br />
                                        </div>
                                        {Worst}
                                    </div>
                                </Col>
                            </Row>
                        </Grid>
                    </div>
                </div>
            </div >
        );
    }
}
