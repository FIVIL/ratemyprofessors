import React, { Component } from 'react';
import { Col, Grid, Row, Glyphicon } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import Gist from 'react-gist';
import history from './history';
import Rating from 'react-rating';
import { Footer } from './Footer'
import { ToastContainer, ToastStore } from 'react-toasts';
import { fail } from 'assert';
export class Proff extends Component {
    displayName = Proff.name;
    constructor(props) {
        super(props);
        this.state = {
            Prof: {}, Comments: {}, loading: true, Anim: true, texts: [],
            Teaching: 0, Marking: 0, HomeWork: 0, Project: 0,
            Moods: 0, RollCall: 0, Exhausting: 0, HandOuts: 0,
            Update: 0, ScapeAtTheEnd: 0, Answering: 0, HardExams: 0,
            Knoledge: 0, OverAll: 0, Text: '', Email: '', ResendEmailButton: false,cmms:[]
        };
        this.FetchData();
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.CheckCookie() ? this.state.Email = this.GetCookie("email") : this.GetCookie("a");
    }
    SetCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toGMTString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    GetCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    CheckCookie() {
        var user = this.GetCookie("email");
        if (user != "") {
            return true;
        } else {
            return false;
        }
    }

    componentDidUpdate(prevProps) {
        if (this.props.match.params.Name != prevProps.match.params.Name) {
            this.setState({ loading: true, texts: [], Prof: {}, Comments: {}, cmms:[] });
            this.FetchData();
        }
    }

    handleInputChange(event) {
        const target = event.target;
        const name = target.name;
        this.setState({
            [name]: target.value
        });
    }
    handleSubmit(event) {
        event.preventDefault();
        fetch('/api/Comments/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                teaching: this.state.Teaching,
                Marking: this.state.Marking,
                HomeWork: this.state.HomeWork,
                Project: this.state.Project,
                Moods: this.state.Moods,
                RollCall: this.state.RollCall,
                Exhausting: this.state.Exhausting,
                HandOuts: this.state.HandOuts,
                Update: this.state.Update,
                ScapeAtTheEnd: this.state.ScapeAtTheEnd,
                Answering: this.state.Answering,
                HardExams: this.state.HardExams,
                Knoledge: this.state.Knoledge,
                OverAll: this.state.OverAll,
                Comments: this.state.Text,
                ProfessorID: this.props.match.params.Name,
                Email: {
                    Address: this.state.Email
                },
                verfied: false,
            })
        }).then(
            response => {
                if (response.status == 200) {
                    if (response.headers.get('MailRes') == '4') {
                        ToastStore.success('نظر شما با موفقیت ثبت شد.', 2000)
                        this.SetCookie("email", this.state.Email, 100);
                    } else if (response.headers.get('MailRes') == '1') {
                        ToastStore.error('شما قبلا نظر خود را در مورد این استاد ثبت کردید.', 3000)
                    } else if (response.headers.get('MailRes') == '2') {
                        ToastStore.warning('ایمیلی حاوی لینک تایید آدرس ایمیل به ایمیل شما ارسال شد، لطفا برای ثبت نظر خود به تایید آدرس ایمیل بپردازید،در صورتی که ایمیلی دریافت نکرده اید پوشه اسپم خود را نیز چک کنید.', 7000)
                        if (!this.CheckCookie()) this.SetCookie("email", this.state.Email, 100);
                    } else {
                        ToastStore.warning('ایمیل تایید آدرس ایمیل قبلا برای شما ارسال شده است، درصورتی که این ایمیل را دریافت نکرده اید بر روی دکه تایید محدد آدرس ایمیل کلید کنید.', 10000)
                        this.setState({ ResendEmailButton: true });
                        setTimeout(() => { this.setState({ ResendEmailButton: false }); }, 20000)
                        if (!this.CheckCookie()) this.SetCookie("email", this.state.Email, 100);
                    }
                } else {
                    ToastStore.error('متاسفانه خطایی رخ داد.', 2000)
                }
            })
        this.setState({
            Teaching: 0, Marking: 0, HomeWork: 0, Project: 0,
            Moods: 0, RollCall: 0, Exhausting: 0, HandOuts: 0,
            Update: 0, ScapeAtTheEnd: 0, Answering: 0, HardExams: 0,
            Knoledge: 0, OverAll: 0, Text: '', Anim: true
        });
        this.FetchData();
    }
    ResendMail() {
        fetch('api/Comments/ResednMail/' + this.state.Email)
            .then(response => response.status = 200 ?
                response.headers.get('MailRes') == '1' ?
                    ToastStore.success('ایمیل تایید آدرس با موفقیت ارسال شد.', 2000)
                    : ToastStore.warning('این ایمیل قبلا تایید شده است.', 2000)
                : ToastStore.error('آدرس ایمیل اشتباه است.', 2000))
            .catch(r => console.log(r));
        this.setState({ ResendEmailButton: false });
    }
    FetchData() {
        fetch('api/Professors/ByID/' + this.props.match.params.Name)
            .then(response => response.json())
            .then(data => {
                this.setState({ Prof: data })
            }).catch(r => console.log(r));

        fetch('api/Comments/ProfAvg/' + this.props.match.params.Name)
            .then(response => response.json())
            .then(data => {
                this.setState({ Comments: data, loading: false });
                var ss = String(data.text);
                var sss = ss.split("$");
                this.setState({ cmms: sss });
            }).catch(r => console.log(r));

    }
    RnderFooter() {
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
    }
    RenderProf(p) {
        return (
            <div className="ProfDetContainer">
                <div className="ProfImageContainer">
                    <img alt={p.fullName}
                        src={p.imageLink == 'nopic' ? require('../imgs/nopic.png') : p.imageLink}
                        className="ProfImage" />
                </div>
                <div className="ProfDetTextContainer">
                    <div className="ProfNameTextContainer">
                        <strong style={{float:'right'}}>نام</strong>
                        <strong style={{float:'left'}}>{p.name}</strong>
                            <br />
                        <strong style={{float:'right'}}>نام خانوادگی</strong>
                        <strong style={{float:'left'}}>{p.lastName}</strong>
                            <br />
                        <strong style={{float:'right'}}>ایمیل</strong>
                        <strong style={{float:'left'}}>{p.privateLink}</strong>
                            <br />
                        <strong style={{float:'right'}}>آدرس WP</strong>
                        <strong style={{float:'left'}}><a target="_blank" href={p.wpLink}>WP</a></strong>
                            <br />
                        <strong style={{float:'right'}}>سایت</strong>
                        <strong style={{float:'left'}}><a target="_blank" href={p.link}>لینک</a></strong>
                            <br />
                        <strong style={{float:'right'}}>امتیاز</strong>
                        {
                            (p.score > 0) ?
                                <strong style={{float:'left'}}>
                                    <label className="text-success"> {p.score} </label>
                                    <Rating
                                        emptySymbol="glyphicon glyphicon-star-empty"
                                        fullSymbol="glyphicon glyphicon-star GGold"
                                        readonly
                                        initialRating={p.score}
                                    />
                                </strong>
                                :
                                <strong style={{float:'left'}}>
                                    <label className="text-danger" dir="ltr"> {p.score} </label>
                                    <Rating
                                        emptySymbol="glyphicon glyphicon-star-empty"
                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                        readonly
                                        initialRating={p.score * -1}
                                        className="NegetiveRate"
                                    />
                                </strong>
                        }
                            <br />
                               
                    </div>
                    <div className="ProfComTextContainer">
                        <h3>توضیحات</h3>
                        <hr />
                        {p.comment}
                    </div>
                </div>
            </div>
        );
    }
    RenderScore(s,sss) {
        return (
            <div>
                <div>
                    <div className="MyColR">
                        <div>
                            <h3 className="text-success text-center">
                                تاثیر مثبت  <Glyphicon glyph="thumbs-up" style={{ marginTop: '3px' }} />
                            </h3>
                            <hr />
                            <div style={{ float: 'right' }}>
                                <strong>مهارت درس دادن</strong>
                                <br />
                                <strong>نمره دادن</strong>
                                <br />
                                <strong>اخلاق</strong>
                                <br />
                                <strong>به روز بودن</strong>
                                <br />
                                <strong>پاسخ دادن به سوالات</strong>
                                <br />
                                <strong>دانش کلی</strong>
                                <br />
                                <strong>مجموع</strong>
                                <br />
                            </div>
                            <div style={{ float: 'left' }}>
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-success"
                                    readonly
                                    initialRating={s.teaching}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-success"
                                    readonly
                                    initialRating={s.marking}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-success "
                                    readonly
                                    initialRating={s.moods}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-success "
                                    readonly
                                    initialRating={s.update}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-success "
                                    readonly
                                    initialRating={s.answering}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-success "
                                    readonly
                                    initialRating={s.knoledge}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-success "
                                    readonly
                                    initialRating={s.overAll}
                                />
                                <br />
                            </div>
                        </div>
                    </div>
                    <div className="MyColL">
                        <div>
                            <h3 className="text-danger text-center">
                                تاثیر منفی <Glyphicon glyph="thumbs-down" style={{ marginTop: '5px' }} />
                            </h3>
                            <hr />
                            <div style={{ float: 'right' }}>
                                <strong>تکلیف زیاد</strong>
                                <br />
                                <strong>پروژه زیاد و سخت</strong>
                                <br />
                                <strong>حضور و غیاب زیاد</strong>
                                <br />
                                <strong>تدریس خواب آور</strong>
                                <br />
                                <strong>جزوه نا مفهوم و گنگ</strong>
                                <br />
                                <strong>غیب شدن در پایان ترم</strong>
                                <br />
                                <strong>امتحانات سخت و طاقت فرسا</strong>
                                <br /></div>
                            <div style={{ float: 'left' }}>
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-danger"
                                    readonly
                                    initialRating={s.homeWork}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-danger"
                                    readonly
                                    initialRating={s.project}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-danger "
                                    readonly
                                    initialRating={s.rollCall}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-danger "
                                    readonly
                                    initialRating={s.exhausting}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-danger "
                                    readonly
                                    initialRating={s.handOuts}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-danger "
                                    readonly
                                    initialRating={s.scapeAtTheEnd}
                                />
                                <br />
                                <Rating
                                    emptySymbol="glyphicon glyphicon-star-empty"
                                    fullSymbol="glyphicon glyphicon-star text-danger "
                                    readonly
                                    initialRating={s.hardExams}
                                />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div className="CommentDetailes">
                    <h3 className="text-center">نظرات</h3>
                    <hr />
                    <div className="CommentDetailesInner">
                        {sss.map(tt =>
                            tt.length>0?
                            <div>
                                <h4>ناشناس گفته:</h4>
                                <p>{tt}</p>
                                <hr />
                                </div>
                                :<div></div>
                        )}
                    </div>
                </div>
            </div>
        );
    }
    render() {
        let foot = this.RnderFooter();
        let Animation = this.state.Anim ? 'InnerElementColse' : 'InnerElementOpen';
        let Animation2 = this.state.Anim ? 'ButtomContainerClose' : 'ButtomContainerOpen';
        let Animation3 = this.state.Anim ? 'FooterAnimColse' : 'FooterAnimOpen';
        let Animation4 = this.state.Anim ? 'TopContainerClose' : 'TopContainerOpen';
        let ProfHeader = this.RenderProf(this.state.Prof);
        let Scores = this.RenderScore(this.state.Comments, this.state.cmms);
        let EmailButton = this.state.ResendEmailButton ? 'ResendEmailButtonVis btn btn-primary' : 'ResendEmailButtonInVis btn btn-primary';
        return (
            <div>
                <button className={EmailButton} onClick={() => { this.setState({ ResendEmailButton: false }); this.ResendMail() }}>ارسال مجدد ایمیل</button>
                <ToastContainer store={ToastStore} position={ToastContainer.POSITION.TOP_RIGHT} />
                <div className="MyContainer">
                    <br />
                    <div className={Animation4}>
                        <div className="BluerAdminHr">
                            {ProfHeader}
                        </div>
                        <div className={Animation}>
                            <form onSubmit={this.handleSubmit}>
                                <div className="container">
                                    <div>
                                        <div className="MyColR">
                                            <h3 className="text-success text-center BluerAdminHr">
                                                امتیاز بیشتر = بهتر  <Glyphicon glyph="thumbs-up" style={{ marginTop: '3px' }} />
                                            </h3>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">مهارت درس دادن</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-success"
                                                        onChange={(rate) => this.setState({ Teaching: rate })}
                                                        initialRating={this.state.Teaching}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">نمره دادن</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-success"
                                                        onChange={(rate) => this.setState({ Marking: rate })}
                                                        initialRating={this.state.Marking}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">اخلاق</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-success"
                                                        onChange={(rate) => this.setState({ Moods: rate })}
                                                        initialRating={this.state.Moods}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">به روز بودن</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-success"
                                                        onChange={(rate) => this.setState({ Update: rate })}
                                                        initialRating={this.state.Update}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">پاسخ دادن به سوالات </label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-success"
                                                        onChange={(rate) => this.setState({ Answering: rate })}
                                                        initialRating={this.state.Answering}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">دانش کلی</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-success"
                                                        onChange={(rate) => this.setState({ Knoledge: rate })}
                                                        initialRating={this.state.Knoledge}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">مجموع</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-success"
                                                        onChange={(rate) => this.setState({ OverAll: rate })}
                                                        initialRating={this.state.OverAll}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div className="MyColL">
                                            <h3 className="text-center text-danger BluerAdminHr">
                                                امتیاز کمتر = بهتر  <Glyphicon glyph="thumbs-up" style={{ marginTop: '3px' }} />
                                            </h3>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">تکلیف زیاد</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        onChange={(rate) => this.setState({ HomeWork: rate })}
                                                        initialRating={this.state.HomeWork}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">پروژه زیاد و سخت</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        onChange={(rate) => this.setState({ Project: rate })}
                                                        initialRating={this.state.Project}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">حضور و غیاب زیاد</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        onChange={(rate) => this.setState({ RollCall: rate })}
                                                        initialRating={this.state.RollCall}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">تدریس خواب آور</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        onChange={(rate) => this.setState({ Exhausting: rate })}
                                                        initialRating={this.state.Exhausting}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">جزوه نا مفهوم و گنگ</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        onChange={(rate) => this.setState({ HandOuts: rate })}
                                                        initialRating={this.state.HandOuts}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">غیب شدن در پایان ترم</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        onChange={(rate) => this.setState({ ScapeAtTheEnd: rate })}
                                                        initialRating={this.state.ScapeAtTheEnd}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <div className="form-control">
                                                    <label className="control-label">امتحانات سخت و طاقت فرسا</label>
                                                    <Rating
                                                        style={{ float: 'left' }}
                                                        emptySymbol="glyphicon glyphicon-star-empty"
                                                        fullSymbol="glyphicon glyphicon-star text-danger"
                                                        onChange={(rate) => this.setState({ HardExams: rate })}
                                                        initialRating={this.state.HardExams}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <br />
                                        <input type="email"
                                            placeholder="آدرس ایمیل شما (برای جلوگیری از ثبت نظرات تکراری برای یک استاد)" value={this.state.Email}
                                            maxlength="100"
                                            onChange={this.handleInputChange} name="Email" className="form-control"
                                            required
                                        />
                                    </div>
                                    <div className="form-group">
                                        <textarea
                                            value={this.state.Text}
                                            onChange={this.handleInputChange}
                                            placeholder="توضیحات بیشتر"
                                            maxlength="399"
                                            name="Text" rows="3" className="form-control" />
                                    </div>
                                    <div className="form-group">
                                        <input type="submit" value="ارسال" className="btn btn-primary SubmitCommentButton" />
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div className="ContainerButton" onClick={() => this.setState({ Anim: !this.state.Anim })}>
                            <strong>
                                برای امتیاز دادن به این استاد اینجا کلیک کنید
                                </strong>
                        </div>
                    </div>
                    <br />
                    <div className={Animation2}>
                        {Scores}
                    </div>
                </div >
                <div className={Animation3}>
                    {foot}
                </div>
            </div >
        );
    };
}