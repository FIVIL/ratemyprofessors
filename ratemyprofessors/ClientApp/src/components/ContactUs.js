import React, { Component } from 'react';
import { Col, Grid, Row, Glyphicon } from 'react-bootstrap'
import { Link } from 'react-router-dom';
import Gist from 'react-gist';
import history from './history';
import { Footer } from './Footer';
import { ToastContainer, ToastStore } from 'react-toasts';
export class ContactUs extends Component {
    displayName = ContactUs.name

    constructor(props) {
        super(props);
        this.state = { text: '', email: '' };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
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
        fetch('api/Contact/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                mailAddress: this.state.email,
                text: this.state.text,
            })
        }).then(response => response.status == 200 ? ToastStore.success('پیام شما با موفقیت ارسال شد.', 2000) : ToastStore.error('متاسفانه خطایی رخ داد.',2000))
            ;
        this.setState({ email: '', text: '' });
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
        let foot = ContactUs.RnderFooter();
        return (
            <div>
                <ToastContainer store={ToastStore} position={ToastContainer.POSITION.TOP_RIGHT} />
                <div className="ContactBack">
                    <br />
                    <div className="inputFomr">
                        <h3 style={{ marginRight: '20px', textAlign: 'center' }}>
                            <Glyphicon glyph="envelope" style={{ marginLeft: '10px' }} />
                            تماس با ما
                </h3>
                        <hr />
                        <form onSubmit={this.handleSubmit} className="CenterFormElement">
                            <div className="form-group">
                                <label className="control-label">
                                    آدرس ایمیل شما <small style={{ color: 'rgb(128, 128, 128)' }}>(در صورت نیاز به پاسخ)</small>
                                </label>
                                <br />
                                <input type="email"
                                    placeholder="a@b.c" value={this.state.email}
                                    maxlength="100"
                                    dir='ltr'
                                    onChange={this.handleInputChange} name="email" className="form-control"
                                />
                            </div>
                            <hr />
                            <div className="form-group">
                                <label className="control-label">
                                    متن پیام
</label>
                                <br />
                                <textarea
                                    value={this.state.text}
                                    onChange={this.handleInputChange}
                                    placeholder="متن پیام"
                                    name="text" rows="7" className="form-control" required />
                            </div>
                            <br />
                            <div className="form-group">
                                <input type="submit" value="ارسال" className="CenterFormElement btn btn-primary" />
                            </div>
                        </form>
                    </div>
                    <br />
                </div>
                {foot}
            </div>
        );
    }
}
