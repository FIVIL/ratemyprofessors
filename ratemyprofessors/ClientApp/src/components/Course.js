import React, { Component } from 'react';
import { Col, Grid, Row, Glyphicon } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { fail } from 'assert';
import Rating from 'react-rating'
import Gist from 'react-gist'
import history from './history'
import { Footer } from './Footer'
export class Course extends Component {
    displayName = Course.name
    constructor(props) {
        super(props);
        this.state = { PageData: [], loading: true };
        this.FetchData();
    }
    componentDidUpdate(prevProps) {
        if (this.props.match.params.ID != prevProps.match.params.ID) {
            this.setState({ PageData: [], loading: true });
            this.FetchData();
        }
    }
    FetchData() {
        fetch('api/Professors/' + this.props.match.params.ID)
            .then(response => response.json())
            .then(data => {
                this.setState({ PageData: data, loading: false });
            }).catch(r => console.log(r));
    }
    static renderDate(ProfData) {
        return (
            <div className="container">
                <hr />
                <div className="LongList">
                    {ProfData.map(p =>
                        <Link className="ProfAndCourse" onClick={() => history.push('/Professor/' + p.id)} to={'/Professor/' + p.id} >
                            <div className="ProfAndCourseInner2">
                                <div style={{ width: '20%', float: 'right' }}>
                                    <img alt={p.fullName}
                                        src={p.imageLink == 'nopic' ? require('../imgs/nopic.png') : p.imageLink}
                                        style={{ width: '99%', height: '150px' }} />
                                </div>
                                <div style={{ width: '78%', float: 'left' }}>
                                    <h3 className="InnerText2">
                                        <text className="InnerRight">{p.fullName}</text>
                                        <div className="InnerLeft" style={{ fontSize: '25px', paddingTop: '5px' }}>
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
                        </Link>
                    )}
                </div>
            </div>
        );
    }
    static RnderFooter() {
        return (<footer className="footer" style={{ background: 'linear-gradient(rgb(68,68,68), rgb(20,20,20))', color: 'white' }}>
            <Grid fluid>
                <Row>
                    <Col md={5}>
                     <Footer />
                    </Col>
                    <Col md={4}>
                        <h3>درباره Grade my teachers</h3>
                        <hr />
                        <p>
                            این سایت در راستای کمک به انتخاب واحد دانشجویان
                                                            دانشگاه صنعتی خواجه نصیر الدین توسی به همت برخی از دانشجویان
                                                                                            دانشکده مهندسی کامپیوتر دانشگاه ایجاد شده است.
                            </p>
                        <p>
                            هدف اصلی این سایت امتیاز دادن به صورت <code>ناشناس</code> و <code>منصفانه</code>   به اساتید دانشکده های مختلف دانشگاه
                            می باشد
                            تا بر اساس این نظرات و امتیاز ها دانشجویان در ترم های  بعد انتخاب های بهتری انجام دهند.
                            </p>
                        <p>
                           
                           
  
                            </p>
                    </Col>
                    <Col md={3}>
                        <h3>به اشتراک گذاری</h3>
                        <hr />
                        <p>به منظور بهتر شدن سایت به کمک شما دوستان برای
                            به اشتراک گذاری
                             سایت بین دانشجویان دانشگاه نیازمندیم
                            در این راستا میتوانید از طریق پیام رسان تلگرام یا شبکه اجتماعی
                                تویتر مارا یاری کنید.</p>
                        <p className="IconCenter">
                            <a href="https://twitter.com/intent/tweet?text=%D8%A8%D9%87+%D8%A7%D8%B3%D8%AA%D8%A7%D8%AF+%D9%87%D8%A7%D8%AA+%D9%86%D9%85%D8%B1%D9%87+%D8%A8%D8%AF%D9%87+%0A+http://grademyteachers.kntu.qmak.ir//" target="_blank" >
                                <img alt="twitter" src={require('../imgs/twitter.svg')} className="ShareLogoImg" />
                            </a>
                            <a href="https://telegram.me/share/url?url=http://grademyteachers.kntu.qmak.ir//" target="_blank">
                                <img alt="telegram" src={require('../imgs/telegram.svg')} className="ShareLogoImg" />
                            </a>
                        </p>
                        <br />
                        <p>
                            برای ارتباط با ما میتوانید از این <Link to={'/contact'} onClick={() => history.push('/contact')} style={{ backgroundColor: 'rgba(256,256,256,0.2)', padding: '1px 3px', borderRadius: '5px' }}>لینک</Link> استفاده نمایید.
                            </p>
                    </Col>
                </Row>
            </Grid>
            <hr />
            <p className="text-center">
                &copy; 2018 هیچ حقی محفوظ نیست.
                    </p>
        </footer>);
    };
    render() {
        let contents = this.state.loading
            ? <h3><em>در حال بارگزاری...</em></h3>
            : Course.renderDate(this.state.PageData);
        let foot = Course.RnderFooter();
        return (
            <div>
                <div className="container">
                    <br />
                    <h1>
                        لیست اساتید درس                     {this.props.match.params.ID2}
                    </h1>
                    {contents}
                    <br />
                </div>
                {foot}
            </div>
        );
    }
}
