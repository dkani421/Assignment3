package src;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Cookie;
import javax.faces.context.FacesContext;
import java.io.IOException;
import java.util.TimeZone;

@WebServlet("/hitCounter")
public class HitCounterServlet extends HttpServlet {
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        // Get the client's IP address
        String clientIP = request.getRemoteAddr();

        // Get the client's time zone
        TimeZone clientTimeZone = TimeZone.getDefault();

        // Retrieve the hit count cookie
        Cookie[] cookies = request.getCookies();
        int hitCount = 1;
        if (cookies != null) {
            for (Cookie cookie : cookies) {
                if ("hitCount".equals(cookie.getName())) {
                    hitCount = Integer.parseInt(cookie.getValue());
                    hitCount++;
                    cookie.setValue(String.valueOf(hitCount));
                    response.addCookie(cookie);
                    break;
                }
            }
        } else {
            Cookie hitCounterCookie = new Cookie("hitCount", "1");
            response.addCookie(hitCounterCookie);
        }

        // Set values to Managed Bean properties
        HitCounterBean hitCounterBean = (HitCounterBean) FacesContext.getCurrentInstance()
                .getExternalContext().getSessionMap().get("hitCounterBean");
        
        hitCounterBean.setHitCount(hitCount);
        hitCounterBean.setClientIP(clientIP);
        hitCounterBean.setClientTimeZone(clientTimeZone.getID());

        // Forward the request to the JSF page
        try {
            request.getRequestDispatcher("/index.xhtml").forward(request, response);
        } catch (ServletException e) {
            // Handle the exception here
        }
    }
}
