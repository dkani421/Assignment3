package src;
import src.HitCounterBean;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;

@ManagedBean
@SessionScoped
public class HitCounterBean {
    private int hitCount;
    private String clientIP;
    private String clientTimeZone;

    // Getter and setter methods for hitCount, clientIP, and clientTimeZone

    public int getHitCount() {
        return hitCount;
    }

    public void setHitCount(int hitCount) {
        this.hitCount = hitCount;
    }

    public String getClientIP() {
        return clientIP;
    }

    public void setClientIP(String clientIP) {
        this.clientIP = clientIP;
    }

    public String getClientTimeZone() {
        return clientTimeZone;
    }

    public void setClientTimeZone(String clientTimeZone) {
        this.clientTimeZone = clientTimeZone;
    }
}
