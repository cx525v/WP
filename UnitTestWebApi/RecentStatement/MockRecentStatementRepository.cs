using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.WebApi.UnitTests.RecentStatement
{
    public class MockRecentStatementRepository
    {
        ICollection<Wp.CIS.LynkSystems.Model.RecentStatement> recentStatement = new List<Wp.CIS.LynkSystems.Model.RecentStatement>();
        public MockRecentStatementRepository()
        {
            recentStatement.Add(new Wp.CIS.LynkSystems.Model.RecentStatement()
            {
                yearId = 2017,
                monthId = 10,
                html_String = @"<table border='0' width='100%'><tr><td nowrap width='62%'><p style='MARGIN: 0px; WORD-SPACING: 0px' align='left'><IMG SRC=images\RBSLynk.gif></p><p style='MARGIN: 0px; WORD-SPACING: 0px'><font size='2'>RBS Lynk Inc.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font></p><p style='MARGIN: 0px; WORD-SPACING: 0px'><font size='2'>600 Morgan Falls Rd.,   St. 260&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font></p><p style='MARGIN: 0px; WORD-SPACING: 0px'><font size='2'>Atlanta,   GA 30350</font></p></td><td nowrap width='38%' valign='top'><table border='1' width='100%' bgcolor='#808080' bordercolorlight='#ffffff'><tr><td nowrap width='100%' valign='top'><p style='MARGIN: 0px; WORD-SPACING: 0px' align='center'><font face='Times New Roman' size='4'>Merchant Processing Statement</font></p><p style='MARGIN: 0px; WORD-SPACING: 0px' align='center'><font face='Times New Roman' size='4'>Location Summary</font></p><p style='MARGIN: 0px; WORD-SPACING: 0px' align='center'><font size='2'>Processing Period: </font><font size='1'>02/01/06 - 02/28/06</font></p></td></tr></table></td></tr></table><p style='MARGIN: 0px; WORD-SPACING: 0px'>&nbsp;</p><table border='0' cellspacing='0' width='100%'><tr><td nowrap width='62%'><p style='MARGIN: 0px; WORD-SPACING: 0px'><FONT size=2><STRONG> Attn: VIVIAN LEO                                                                                                                                                                      </STRONG></FONT></p>
<p style='MARGIN: 0px; WORD-SPACING: 0px'><FONT size=2><STRONG> CITY OF BOSTON/EXCISE TAX                                                                                                                                                    </STRONG></FONT></p>
<p style='MARGIN: 0px; WORD-SPACING: 0px'><FONT size=2><STRONG> M-35 CITY HALL TREASURY DEPARTMENT                                                                                                                                  </STRONG></FONT></p>
<p style='MARGIN: 0px; WORD-SPACING: 0px'><FONT size=2><STRONG> BOSTON   , MA   02201                                                                                                                                                            </STRONG></FONT></p>
</td><td nowrap width='38%'><p style='MARGIN: 0px; WORD-SPACING: 0px'><FONT size=2><STRONG>Customer Number:&nbsp;1000332788</STRONG></FONT></p><p style='MARGIN: 0px; WORD-SPACING: 0px'><FONT size=2><STRONG>Merchant Number:&nbsp;542929801430265</STRONG></FONT></p></td></tr></table><p style='MARGIN: 0px; WORD-SPACING: 0px'>&nbsp;</p><p style='MARGIN: 0px; WORD-SPACING: 0px' align='center'><font size=2 color='#ff0000'>IMPORTANT NOTICE</font></p><p style='MARGIN: 0px; WORD-SPACING: 0px'>&nbsp;</p><div align='justify'><table border='0' cellspacing='0' width='100%'><tr><tr><td colspan=100% align=left><font size=1>                        ***Visa Network Pricing Change***                                                                                      
 Effective April 7, 2006, the following Visa Network pricing will be changed.                                              
 The current and new rates are listed below:                                                                                                                
 Visa Level II                             Current Rate           New Rate                                                    
              Commercial Business            1.90%+$.10          2.00%+$.10                                                  
              Commercial Purchasing          1.90%+$.10          2.00%+$.10                                                  
 Visa Check Card                                                                                                                                                                        
              Small Ticket Credit            1.65%+$.04          1.65%+$.04                                                  
              Small Ticket Debit             1.60%+$.04          1.55%+$.04                                                  
                                                                                                                                                                                                        
                         ***Visa Expansion of Small Ticket***                                                                              
 Effective April 8, 2006, Visa will be eliminating their Express Payment Service                                        
 interchange category and expanding existing Small Ticket to include seven new market                              
 segments. Signature requirements will be eliminated on qualified transactions less                                  
 than $25. A free software download is required to activate non-signature receipts.                                  
 Please call the RBS Lynk Help Desk at 800-859-5965, option 2 to request the download.                            
 Note the download may take up to 30 minutes to complete.                                                                                      
                                                                                                                                                                                                        
              ***MasterCard Expansion of Quick Payment Service Program***                                                      
 Effective April 7, 2006, MasterCard will be expanding eligible merchant categories to                            
 include Limousines & Taxicabs.                                                                                                                                          
                                                                                                                                                                                                        
                                    ***Sneak Peek***                                                                                                
 RBS Lynk plans to add Visa prepaid card balance inquiry and partial authorization                                    
 functionality to select Point of Sale systems this year.  Prepaid card support is                                    
 another benefit of processing with RBS Lynk.                                                                                                              
                                                                                                                                                                                                        
 ***For additional information regarding these programs and rates, please visit                                          
 www.lynkassist.com. If you have any other questions, please don't hesitate to contact                            
 Customer Care at 800-859-5965, option 3. We appreciate your business!                                                            

<!--Begin TranSummary Title-->
<p>&nbsp;</p><table border=1 cellspacing=0 width=20% cellspacing=1><tr><td nowrap width=100%><font size=2><b>TRANSACTION SUMMARY: TERMINAL LK404587</b></font></td></tr></table><br>
<!--End TranSummary Title-->

<!--Begin TranSummary Table Heading-->

<div align=right><table border=0 cellspacing=0 width=100%>
<tr><td nowrap align=left bgcolor=#C0C0C0><font size=1><b>Card                </b></font></td>
<td nowrap colspan=2 align=right bgcolor=#C0C0C0><font size=1><b>------Sales------ </b></font></td>
<td nowrap colspan=2 align=right bgcolor=#C0C0C0><font size=1><b> Returns/Credits </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b> Total   </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>  Net Sales </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>  Discount </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>Item Fees </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>  Process </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>     Total </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>     Paid By </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>     Paid By</b></font></td></tr>
<tr><td nowrap align=left bgcolor=#C0C0C0><font size=1><b>Type                </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>Items </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>     Amount </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>Items </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>    Amount </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b> Items   </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>     Amount </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>       Due </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>      Due </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b> Fees Due </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>       Due </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>    RBS Lynk </b></font></td>
<td nowrap  align=right bgcolor=#C0C0C0><font size=1><b>   3rd Party</b></font></td></tr>

<!--End TranSummary Table Heading-->
<!--Begin TranSummary Detail-->
<!--Detail Row-->
<tr><td nowrap align=left><font size=1>Visa                </font></td>
<td nowrap align=right><font size=1> 1044</font></td>
<td nowrap align=right><font size=1>   55,201.89 </font></td>
<td nowrap align=right><font size=1>    0</font></td>
<td nowrap align=right><font size=1>        .00 </font></td>
<td nowrap align=right><font size=1>  1044</font></td>
<td nowrap align=right><font size=1>   55,201.89 </font></td>
<td nowrap align=right><font size=1>  1,163.28 </font></td>
<td nowrap align=right><font size=1>   170.80 </font></td>
<td nowrap align=right><font size=1>   187.92 </font></td>
<td nowrap align=right><font size=1>  1,522.00 </font></td>
<td nowrap align=right><font size=1>   53,679.89</font></td>
<td nowrap align=right><font size=1></font></td></tr>
<!--Begin TranSummary Detail-->
<!--Detail Row-->
<tr><td nowrap align=left><font size=1>MasterCard          </font></td>
<td nowrap align=right><font size=1>  676</font></td>
<td nowrap align=right><font size=1>   39,210.55 </font></td>
<td nowrap align=right><font size=1>    0</font></td>
<td nowrap align=right><font size=1>        .00 </font></td>
<td nowrap align=right><font size=1>   676</font></td>
<td nowrap align=right><font size=1>   39,210.55 </font></td>
<td nowrap align=right><font size=1>    794.21 </font></td>
<td nowrap align=right><font size=1>    89.02 </font></td>
<td nowrap align=right><font size=1>   121.68 </font></td>
<td nowrap align=right><font size=1>  1,004.91 </font></td>
<td nowrap align=right><font size=1>   38,205.64</font></td>
<td nowrap align=right><font size=1></font></td></tr>
<!--Begin TranSummary Detail-->
<!--Detail Row-->
<tr><td nowrap align=left><font size=1>  Bankcard Sub-total</font></td>
<td nowrap align=right><font size=1> 1720</font></td>
<td nowrap align=right><font size=1>   94,412.44 </font></td>
<td nowrap align=right><font size=1>    0</font></td>
<td nowrap align=right><font size=1>        .00 </font></td>
<td nowrap align=right><font size=1>  1720</font></td>
<td nowrap align=right><font size=1>   94,412.44 </font></td>
<td nowrap align=right><font size=1>  1,957.49 </font></td>
<td nowrap align=right><font size=1>   259.82 </font></td>
<td nowrap align=right><font size=1>   309.60 </font></td>
<td nowrap align=right><font size=1>  2,526.91 </font></td>
<td nowrap align=right><font size=1>   91,885.53 </font></td>
<td nowrap align=right><font size=1>         .00</font></td></tr>

<!--Begin TranSummary Total--><!--Total Row-->
<tr><td colspan=100%><hr></td></tr><tr><td nowrap width='11%' align='center'><font size='1'></font><p align='right'><b><font size='1'>Total              </font></b></td>
<td nowrap width='6%' align='right'><font size='1'><b>  1720</b></font></td>
<td nowrap width='5%' align='right'><font size='1'><b>   94,412.44 </b></font></td>
<td nowrap width='11%' align='right'><font size='1'><b>    0</b></font></td>
<td nowrap width='6%' align='right'><font size='1'><b>        .00 </b></font></td>
<td nowrap width='5%' align='right'><font size='1'><b>  1720</b></font></td>
<td nowrap width='6%' align='right'><font size='1'><b>   94,412.44 </b></font></td>
<td nowrap width='5%' align='right'><font size='1'><b>  1,957.49 </b></font></td>
<td nowrap width='11%' align='right'><font size='1'><b>   259.82 </b></font></td>
<td nowrap width='11%' align='right'><font size='1'><b>   309.60 </b></font></td>
<td nowrap width='11%' align='right'><font size='1'><b>  2,526.91 </b></font></td>
<td nowrap width='11%' align='right'><font size='1'><b>   91,885.53 </b></font></td>
<td nowrap width='11%' align='right'><font size='1'><b>         .00</b></font></td>
</tr>
<!--End Row--><!--End TranSummary Total-->
</table></div><p>&nbsp;</p>
<!--Begin Card Fee Title-->
<p>&nbsp;</p><table border=1 cellspacing=0 width=20% cellspacing=1><tr><td nowrap width=100%><font size=2><b>CARD FEE SUMMARY: TERMINAL LK404587</b></font></td></tr></table><br>
<!--End Card Fee Title-->

<!--Begin Card Fee Heading-->

<div align=right><table border=0 cellspacing=0 width=100%>
<tr>
<td nowrap width=9% align=left bgcolor=#C0C0C0><b><font size=1>Card         </b></font></td>
<td nowrap width=9% align=left bgcolor=#C0C0C0><font size=1><b>Interchange            </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0>&nbsp;</td>
<td nowrap width=18% align=right colspan=2 bgcolor=#C0C0C0><font size=1><b> ---Discount Fees--  </b></font></td>
<td nowrap width=27% align=right colspan=3 bgcolor=#C0C0C0><font size=1><b>    ----------Item Fees----------  </b></font></td>
<td nowrap width=18% align=right colspan=2 bgcolor=#C0C0C0><font size=1><b>     --Processing Fees--  </b></font></td>
<td nowrap width=10% align=right bgcolor=#C0C0C0><b><font size=1>       Total</b></font></td></tr>
<tr>
<td nowrap width=9% align=left bgcolor=#C0C0C0><b><font size=1>Type         </b></font></td>
<td nowrap width=9% align=left bgcolor=#C0C0C0><font size=1><b>Level                  </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><font size=1><b>   Amount  </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><b><font size=1>  Rate   </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><b><font size=1>    Amount  </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><b><font size=1>     Items  </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><p align=right><b><font size=1>      Fee  </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><b><font size=1>    Amount  </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><b><font size=1>       Fee  </b></font></td>
<td nowrap width=9% align=right bgcolor=#C0C0C0><b><font size=1>      Amount  </b></font></td>
<td nowrap width=10% align=right bgcolor=#C0C0C0><b><font size=1>         Due</b></font></td></tr>

<!--End CardFee Heading-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>Visa         </font></td>
<td nowrap align=left><font size=1>EIRF DB            </font></td>
<td nowrap align=right><font size=1>    28,561.96  </font></td>
<td nowrap align=right><font size=1>  1.84 % </font></td>
<td nowrap align=right><font size=1>    526.60  </font></td>
<td nowrap align=right><font size=1>       664  </font></td>
<td nowrap align=right><font size=1>     .200  </font></td>
<td nowrap align=right><font size=1>    132.80  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>      119.52  </font></td>
<td nowrap align=right><font size=1>      778.92</font></td>
</tr><!--End Row-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>Visa         </font></td>
<td nowrap align=left><font size=1>EIRF               </font></td>
<td nowrap align=right><font size=1>    25,701.73  </font></td>
<td nowrap align=right><font size=1>  2.39 % </font></td>
<td nowrap align=right><font size=1>    615.12  </font></td>
<td nowrap align=right><font size=1>       350  </font></td>
<td nowrap align=right><font size=1>     .100  </font></td>
<td nowrap align=right><font size=1>     35.00  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>       63.00  </font></td>
<td nowrap align=right><font size=1>      713.12</font></td>
</tr><!--End Row-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>Visa         </font></td>
<td nowrap align=left><font size=1>Business Elec      </font></td>
<td nowrap align=right><font size=1>       938.20  </font></td>
<td nowrap align=right><font size=1>  2.30 % </font></td>
<td nowrap align=right><font size=1>     21.56  </font></td>
<td nowrap align=right><font size=1>        30  </font></td>
<td nowrap align=right><font size=1>     .100  </font></td>
<td nowrap align=right><font size=1>      3.00  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>        5.40  </font></td>
<td nowrap align=right><font size=1>       29.96</font></td>
</tr><!--End Row-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>MasterCard   </font></td>
<td nowrap align=left><font size=1>Int'l Standard     </font></td>
<td nowrap align=right><font size=1>        27.81  </font></td>
<td nowrap align=right><font size=1>  2.23 % </font></td>
<td nowrap align=right><font size=1>       .62  </font></td>
<td nowrap align=right><font size=1>         2  </font></td>
<td nowrap align=right><font size=1>     .100  </font></td>
<td nowrap align=right><font size=1>       .20  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>         .36  </font></td>
<td nowrap align=right><font size=1>        1.18</font></td>
</tr><!--End Row-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>MasterCard   </font></td>
<td nowrap align=left><font size=1>Merit 1            </font></td>
<td nowrap align=right><font size=1>    13,261.52  </font></td>
<td nowrap align=right><font size=1>  2.05 % </font></td>
<td nowrap align=right><font size=1>    271.27  </font></td>
<td nowrap align=right><font size=1>       222  </font></td>
<td nowrap align=right><font size=1>     .100  </font></td>
<td nowrap align=right><font size=1>     22.20  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>       39.96  </font></td>
<td nowrap align=right><font size=1>      333.43</font></td>
</tr><!--End Row-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>MasterCard   </font></td>
<td nowrap align=left><font size=1>Merit 1 DB         </font></td>
<td nowrap align=right><font size=1>    14,770.53  </font></td>
<td nowrap align=right><font size=1>  1.74 % </font></td>
<td nowrap align=right><font size=1>    256.28  </font></td>
<td nowrap align=right><font size=1>       357  </font></td>
<td nowrap align=right><font size=1>     .160  </font></td>
<td nowrap align=right><font size=1>     57.12  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>       64.26  </font></td>
<td nowrap align=right><font size=1>      377.66</font></td>
</tr><!--End Row-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>MasterCard   </font></td>
<td nowrap align=left><font size=1>WC Merit I         </font></td>
<td nowrap align=right><font size=1>     6,678.60  </font></td>
<td nowrap align=right><font size=1>  2.15 % </font></td>
<td nowrap align=right><font size=1>    143.29  </font></td>
<td nowrap align=right><font size=1>        67  </font></td>
<td nowrap align=right><font size=1>     .100  </font></td>
<td nowrap align=right><font size=1>      6.70  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>       12.06  </font></td>
<td nowrap align=right><font size=1>      162.05</font></td>
</tr><!--End Row-->
<!--Begin CardFeeSummary Detail--> 
<!--Begin Row-->
<tr>
<tr><td nowrap align=left><font size=1>MasterCard   </font></td>
<td nowrap align=left><font size=1>Data Rate 1        </font></td>
<td nowrap align=right><font size=1>     4,472.09  </font></td>
<td nowrap align=right><font size=1>  2.74 % </font></td>
<td nowrap align=right><font size=1>    122.75  </font></td>
<td nowrap align=right><font size=1>        28  </font></td>
<td nowrap align=right><font size=1>     .100  </font></td>
<td nowrap align=right><font size=1>      2.80  </font></td>
<td nowrap align=right><font size=1>      .180  </font></td>
<td nowrap align=right><font size=1>        5.04  </font></td>
<td nowrap align=right><font size=1>      130.59</font></td>
</tr><!--End Row-->

<tr><td colspan=100%><hr></td></tr><!--Begin CardFee Summary Total--> 
<!--Begin Row-->
<tr><td nowrap align=right><font size=1><b>Total        </font></td>
<td nowrap align=left><font size=1><b>                   </font></td>
<td nowrap align='right'><font size='1'><b>    94,412.44  </font></td>
<td nowrap align='right'><font size='1'><b>         </font></td>
<td nowrap align='right'><font size='1'><b>  1,957.49  </font></td>
<td nowrap align='right'><font size='1'><b>     1,720  </font></td>
<td nowrap align='right'><font size='1'><b>           </font></td>
<td nowrap align='right'><font size='1'><b>    259.82  </font></td>
<td nowrap align='right'><font size='1'><b>            </font></td>
<td nowrap align='right'><font size='1'><b>      309.60  </font></td>
<td nowrap align='right'><font size='1'><b>    2,526.91</font></td>
</tr><!--End Row--><!--End CardFeeSummary Total--></table></div>

<!--Begin of Summary Of All Fees Title-->
<p>&nbsp;</p><table border=1 cellspacing=0 width=20% cellspacing=1><tr><td nowrap width=100%><font size=2><b>SUMMARY OF ALL FEES: TERMINAL LK404587                                </b></font></td></tr></table><br>
<!--End Summary Of All Fees-->

<!--Begin of SummaryOfAllFee Section-->

<!--Begin of SummaryOfAllFee Heading-->

<table border=0 cellspacing=0 width=100%>
<tr>
<td nowrap width=64% align=left bgcolor=#C0C0C0>&nbsp;</td>
  <td nowrap width=10% align=right bgcolor=#C0C0C0>&nbsp;</td>  <td nowrap width=10% align=right bgcolor=#C0C0C0>&nbsp;</td>  <td nowrap width=16% align=right bgcolor=#C0C0C0><font size=1><b>                     Total</b></font></td>
</tr>
<tr>
  <td nowrap align=left bgcolor=#C0C0C0><font size=1><b>Description                                                                                       </b></font></td>
  <td nowrap align=right bgcolor=#C0C0C0><font size=1><b>Items </b></font></td>
  <td nowrap align=right bgcolor=#C0C0C0><b><font size=1>       Fee  </b></font></td>
  <td nowrap align=right bgcolor=#C0C0C0><b><font size=1>                       Due</b></font></td>
</tr>

<!--End SummaryOfAllHeading-->
<!--Begin SummaryOfAllFees Detail--><!--Begin Row--><tr>
<td nowrap align=left><font size=1> Total Card Fees                                                                                  </font></td>
<td nowrap align=right><font size=1>      </font></td>
<td nowrap align=right><font size=1>            </font></td>
  <td nowrap align=right>  <font size=1>                  2,526.91</font></td>
</tr><!--End Row-->
<!--Begin SummaryOfAllFees Detail--><!--Begin Row--><tr>
<td nowrap align=left><font size=1> Administrative Fee                                                                               </font></td>
<td nowrap align=right><font size=1>    1 </font></td>
<td nowrap align=right><font size=1>       .00  </font></td>
  <td nowrap align=right>  <font size=1>                       .00</font></td>
</tr><!--End Row-->
<!--Begin SummaryOfAllFees Detail--><!--Begin Row--><tr>
<td nowrap align=left><font size=1> Bankcard Auth Fee                                                                                </font></td>
<td nowrap align=right><font size=1> 1744 </font></td>
<td nowrap align=right><font size=1>       .00  </font></td>
  <td nowrap align=right>  <font size=1>                       .00</font></td>
</tr><!--End Row-->
<!--Begin SummaryOfAllFees Detail--><!--Begin Row--><tr>
<td nowrap align=left><font size=1> Batch Header Fees                                                                                </font></td>
<td nowrap align=right><font size=1>   28 </font></td>
<td nowrap align=right><font size=1>       .00  </font></td>
  <td nowrap align=right>  <font size=1>                       .00</font></td>
</tr><!--End Row-->
<!--Begin SummaryOfAllFees Detail--><!--Begin Row--><tr>
<td nowrap align=left><font size=1> Customer Service Fee                                                                             </font></td>
<td nowrap align=right><font size=1>    1 </font></td>
<td nowrap align=right><font size=1>       .00  </font></td>
  <td nowrap align=right>  <font size=1>                       .00</font></td>
</tr><!--End Row-->

<tr><td nowrap colspan=100%><hr></td></tr>
<!--Begin SummaryOfAllFees Totals-->
<tr>
  <td nowrap colspan=3 align=left>  <font size=1><b> Total Fees                                                                                                         </b></font></td>
  <td nowrap align=right>  <font size=1><b>                  2,526.91</b></font></td></tr>
<tr><td nowrap colspan=100%><hr></td></tr>
<!--Begin SummaryOfAllFees Totals-->
<tr>
  <td nowrap colspan=3 align=left>  <font size=1><b> Total Fees Charged for this Terminal to Bank Routing/Transit# 211070175 DDA# 1101011715        on 03/01/06:        </b></font></td>
  <td nowrap align=right>  <font size=1><b>                  2,526.91</b></font></td></tr>
<tr><td nowrap colspan=3 align=left><font size=1 color=red ><b> Total Fees Charged                                                                                                 </b></font></td>
<td nowrap align=right><font size=1 color=red ><b>                  2,526.91</b></font></td></tr>
</table>
<!--End SummaryOfAllFees Totals-->
<!--End Row--><!--End of SummaryOfAllFee Section--><!--Begin DailyDeposit Announcement Line-->
<table width=100%>
<!--Begin Horizontal Rule--><tr><td nowrap colspan=100%><hr></td></tr><!--End Horizontal Rule-->
<!--Begin Row-->
<tr><td nowrap width=34%><font size=1 color=red>
DAILY DEPOSIT DETAIL                         </font></td>
<td nowrap width=33%><font size=1 color=red>
Bank Routing/Transit#: 211070175             </font></td>
<td nowrap width=33%><font size=1 color=red>DDA#  1137491318</font></td><tr>
<!--End Row-->
<tr><td nowrap colspan=100%><hr></td></tr>
<!--End Horizontal Rule-->
</table>
<!--End DailyDepositDetail Announcement Line-->
<!--Begin Heading-->
<table width=100% border=0 bgcolor=#C0C0C0 cellspacing=0>
<tr>
<td nowrap width=9% align=left><font size=1><b>Exception                  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>Terminal </b></font></td>
  <td nowrap width=7% align=right><font size=1><b> Paid   </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  Batch   </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>   Batch      </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>    Card  </b></font></td>
<td width=14% nowrap colspan=2 align=right><font size=1><b> ------Sales------  </b></font></td>
<td width=14% nowrap colspan=2 align=right><font size=1><b>Returns/Credits  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>      Net </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>    Total  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>    Paid by  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>   Paid by</b></font></td>
</tr></table>
<!--End Heading Section-->
<!--Begin Heading-->
<table width=100% border=0 bgcolor=#C0C0C0 cellspacing=0>
<tr>
<td nowrap width=9% align=left><font size=1><b>Description                </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>   Id    </b></font></td>
  <td nowrap width=7% align=right><font size=1><b> Date   </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>   Date   </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  Number      </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>    Type  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b> Items  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>    Amount  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>Items </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>   Amount  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>    Sales </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>Collected  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>   RBS Lynk  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b> 3rd Party</b></font></td>
</tr></table>
<!--End Heading Section-->
<table width=100% border=0 cellspacing=0>
<!--Begin Row-->
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1> 404587 </font></td>
  <td nowrap width=7% align=right><font size=1>02/01/06 </font></td>
  <td nowrap width=7% align=right><font size=1> 02/01/06 </font></td>
  <td nowrap width=7% align=right><font size=1>    100217</font></td>
  <td nowrap width=7% align=right><font size=1>    BankCard  </font></td>
  <td nowrap width=7% align=right><font size=1>    65  </font></td>
  <td nowrap width=7% align=right><font size=1>  6,098.46  </font></td>
  <td nowrap width=7% align=right><font size=1>    0 </font></td>
  <td nowrap width=7% align=right><font size=1>      .00  </font></td>
  <td nowrap width=7% align=right><font size=1>  6,098.46 </font></td>
  <td nowrap width=7% align=right><font size=1>     .00  </font></td>
  <td nowrap width=7% align=right><font size=1>   6,098.46  </font></td>
  <td nowrap width=7% align=right><font size=1>       .00</font></td>
</tr></table>
<!--End Row-->
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>     </font></td>
  <td nowrap width=7% align=right><font size=1><b>Batch Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  6,098.46 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>   6,098.46  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--End TotalCR Row-->
</table>
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>  </font></td>
  <td nowrap width=7% align=right><font size=1><b>Terminal Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  6,098.46 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>   6,098.46  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--End TotalCR Row-->
</table>
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>     </font></td>
  <td nowrap width=7% align=right><font size=1><b>Daily Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  6,098.46 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>   6,098.46  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--Begin Horizontal Rule-->
<tr><td colspan=100% nowrap>
<hr>
</td>
</tr>
<!--End Horizontal Rule-->
<!--End TotalCR Row-->
</table>
<table width=100% border=0 cellspacing=0>
<!--Begin Row-->
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1> 404587 </font></td>
  <td nowrap width=7% align=right><font size=1>02/02/06 </font></td>
  <td nowrap width=7% align=right><font size=1> 02/02/06 </font></td>
  <td nowrap width=7% align=right><font size=1>    100218</font></td>
  <td nowrap width=7% align=right><font size=1>    BankCard  </font></td>
  <td nowrap width=7% align=right><font size=1>    42  </font></td>
  <td nowrap width=7% align=right><font size=1>  1,718.12  </font></td>
  <td nowrap width=7% align=right><font size=1>    0 </font></td>
  <td nowrap width=7% align=right><font size=1>      .00  </font></td>
  <td nowrap width=7% align=right><font size=1>  1,718.12 </font></td>
  <td nowrap width=7% align=right><font size=1>     .00  </font></td>
  <td nowrap width=7% align=right><font size=1>   1,718.12  </font></td>
  <td nowrap width=7% align=right><font size=1>       .00</font></td>
</tr></table>
<!--End Row-->
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>     </font></td>
  <td nowrap width=7% align=right><font size=1><b>Batch Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  1,718.12 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>   1,718.12  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--End TotalCR Row-->
</table>
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>  </font></td>
  <td nowrap width=7% align=right><font size=1><b>Terminal Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  1,718.12 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>   1,718.12  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--End TotalCR Row-->
</table>
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>     </font></td>
  <td nowrap width=7% align=right><font size=1><b>Daily Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  1,718.12 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>   1,718.12  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--Begin Horizontal Rule-->
<tr><td colspan=100% nowrap>
<hr>
</td>
</tr>
<!--End Horizontal Rule-->
<!--End TotalCR Row-->
</table>
<table width=100% border=0 cellspacing=0>
<!--Begin Row-->
<tr><td nowrap width=9% align=left><font size=1>Transaction Review         </font></td>
  <td nowrap width=7% align=right><font size=1> 404587 </font></td>
  <td nowrap width=7% align=right><font size=1>02/03/06 </font></td>
  <td nowrap width=7% align=right><font size=1> 02/03/06 </font></td>
  <td nowrap width=7% align=right><font size=1>    200219</font></td>
  <td nowrap width=7% align=right><font size=1>    BankCard  </font></td>
  <td nowrap width=7% align=right><font size=1>    41  </font></td>
  <td nowrap width=7% align=right><font size=1>  1,328.64  </font></td>
  <td nowrap width=7% align=right><font size=1>    0 </font></td>
  <td nowrap width=7% align=right><font size=1>      .00  </font></td>
  <td nowrap width=7% align=right><font size=1>  1,328.64 </font></td>
  <td nowrap width=7% align=right><font size=1>     .00  </font></td>
  <td nowrap width=7% align=right><font size=1>        .00  </font></td>
  <td nowrap width=7% align=right><font size=1>       .00</font></td>
</tr></table>
<!--End Row-->
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>     </font></td>
  <td nowrap width=7% align=right><font size=1><b>Batch Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  1,328.64 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>        .00  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--End TotalCR Row-->
</table>
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>              </font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>            </font></td>
  <td nowrap width=7% align=right><font size=1>  </font></td>
  <td nowrap width=7% align=right><font size=1><b>Terminal Total </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>  1,328.64 </b></font></td>
  <td nowrap width=7% align=right><font size=1>          </font></td>
  <td nowrap width=7% align=right><font size=1><b>        .00  </b></font></td>
  <td nowrap width=7% align=right><font size=1><b>       .00</b></font></td>
</tr><!--End TotalCR Row-->
</table>
<!--Begin TotalCR Row-->
<table width=100% border=0 cellspacing=0>
<tr><td nowrap width=9% align=left><font size=1>                           </font></td>
  <td nowrap width=7% align=right><font size=1>        </font></td>
  <td nowrap width=7% align=right><font size=1>         </font></td>
  <td nowrap width=7% align=right><font size=1>          "
            });
        }

        public ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> GetMockData()
        {
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expected = new ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>>()
            {
                Result = recentStatement
            };
            return expected;
        }
    }
}
