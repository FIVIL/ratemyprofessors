import React, { Component } from 'react';
import { Glyphicon, Col, Grid, Row } from 'react-bootstrap';
export class Footer extends Component {
    displayName = Footer.name
    render() {
        return (
            <div>
                <br />
                <Grid fluid>
                    <Row>
                        <Col md={6}>
                            <img src={require('../imgs/fair.svg')} alt="منصفانه" style={{ width: '99%', height: '120px', margin: 'auto' }} />
                            <p style={{ paddingTop: '10px' }}>لطفا منصافانه نظرات خودرا بیان کنید.
                            تا دانشجویان دیگر بتوانند از نظرات شما در انتخاب واحدهای شان استفاده کنند.</p>
                        </Col>
                        <Col md={6}>
                            <img src={require('../imgs/anon.svg')} alt="ناشناس" style={{ width: '99%', height: '120px', margin: 'auto' }} />

                            <p style={{ paddingTop: '10px' }}>اطلاعات هویتی شما نظیر آدرس ایمیل فقط و فقط برای جلوگیری از اسپم دریافت میشوند، و نظرات شما به صورت
                                    کاملا ناشناس در سایت درج خواهند شد.</p>
                        </Col>
                    </Row>
                </Grid>
            </div>
        );
    }
}