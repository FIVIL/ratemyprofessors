import React, { Component } from 'react';
import { Glyphicon, Col, Grid, Row } from 'react-bootstrap'
import { fail } from 'assert';
import { Link } from 'react-router-dom';
import Rating from 'react-rating'
import Gist from 'react-gist'
import history from './history'
import { Footer } from './Footer'
var facName = '';
export class Faculties extends Component {
    displayName = Faculties.name
    constructor(props) {
        super(props);
        this.state = { PageData: [], loading: true, CourseData: [] };
        facName = props.match.params.Name;
        this.FetchData();
    }
    componentDidUpdate(prevProps) {
        if (this.props.match.params.Name != prevProps.match.params.Name) {
            facName = this.props.match.params.Name;
            this.state.loading = true;
            this.setState({ PageData: [], loading: true, CourseData: [] });
            this.FetchData();
        }
    }
    FetchData() {
        fetch('api/Courses/' + facName)
            .then(response => response.json())
            .then(data => {
                this.setState({ PageData: data });
            }).catch(r => console.log(r));
        fetch('api/Professors/ProfFac/' + facName)
            .then(response => response.json())
            .then(data => {
                this.setState({ CourseData: data });
            }).catch(r => console.log(r));
    }
    renderDate(CoursesData, ProfData) {
        return (
            <div className="container">
                <Grid fluid>
                    <Row>
                        <Col md={6}>
                            <h3>
                                لیست درس ها
                         <button onClick={() => history.push('/AddCourse/' + facName)} className="btn btn-info" style={{ float: 'left', margin: '0px 5px' }}><Glyphicon glyph='plus' />  درس جدید</button>
                            </h3>
                            <hr />
                            <div className="LongList">
                                {CoursesData.map(c =>
                                    <div className="ProfAndCourse" onClick={() => history.push('/course/' + c.id + '/' + c.name + ' ' + this.props.match.params.Name2)}>
                                        <div className="ProfAndCourseInner">
                                            <h2 className="InnerText" style={{ textAlign: 'center' }}>{c.name}</h2>
                                        </div>
                                    </div>
                                )}
                            </div>
                        </Col>
                        <Col md={6}>
                            <h3>
                                لیست استاد ها
                                                    <button onClick={() => history.push('/AddProf/' + facName)} className="btn btn-info" style={{ float: 'left', margin: '0px 5px' }}><Glyphicon glyph='plus' /> استاد جدید</button>
                            </h3>
                            <hr />
                            <div className="LongList">
                            {ProfData.map(p =>
                                <div className="ProfAndCourse" onClick={() => history.push('/Professor/' + p.id)}>
                                    <div className="ProfAndCourseInner">
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
                        </Col>
                    </Row>
                </Grid>
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
        let contents = this.renderDate(this.state.PageData, this.state.CourseData);
        let foot = Faculties.RnderFooter();
        return (
            <div>
                <div className="container">
                    <br />
                    <h1>
                        <Glyphicon glyph={String(this.props.match.params.Name3)} style={{ marginLeft: '10px' }} />
                        {this.props.match.params.Name2}
                    </h1>
                    <hr />
                    {contents}
                    <br />
                </div>
                {foot}
            </div>
        );
    }
}
